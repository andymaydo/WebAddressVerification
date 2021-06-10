using Aplication;
using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class WebAddressVerificationService : IWebAddressVerification
    {
        private string _sqlConnString;
        public WebAddressVerificationService(IConfiguration configuration)
        {
            _sqlConnString = configuration["ConnString"];
        }
        public  List<CountryCode> CountryCodeGetAll()
        {
            string sql = "pCountryCode_GetAll";

            var p = new DynamicParameters();



            using (var connection = new SqlConnection(_sqlConnString))
            {
                connection.Open();
                var result = connection.Query<CountryCode>(sql, p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public  (int Status, List<ResponseDetail> Detail) RequestValidate(Request a)
        {
            string sql = "pValidate_Request";

            var p = new DynamicParameters();
            p.Add("CountryCodeISO2", a.CountryCodeISO2);
            p.Add("State ", a.State);
            p.Add("City ", a.City);
            p.Add("Zip", a.Zip);
            p.Add("Street", a.Street);
            p.Add("StreetNumber", a.StreetNumber);
            p.Add("CallerId", a.CallerId);
            p.Add("RequestId", a.RequestId, direction: ParameterDirection.Output);
            p.Add("ResponseStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var conection = new SqlConnection(_sqlConnString))
            {
                conection.Open();

                var detail = conection.Query<ResponseDetail>(sql, p, commandType: CommandType.StoredProcedure);
                int status = p.Get<int>("ResponseStatus");
                return (status, detail.ToList());
            }


        }
        public  List<Report> ReportRequestGet(int? Status, DateTime FromDate, DateTime ToDate, int CallerId, string Street)
        {
            string sql = "pReportRequest";

            var p = new DynamicParameters();
            p.Add("From", FromDate, dbType: DbType.DateTime);
            p.Add("To", ToDate.AddDays(1), dbType: DbType.DateTime);
            p.Add("Street", Street);
            if (Status == -1)
            {
                p.Add("Status", DBNull.Value, dbType: DbType.Int32);
            }
            else
            {
                p.Add("Status", Status, dbType: DbType.Int32);
            }


            p.Add("CallerId", CallerId, dbType: DbType.Int32);

            using (var conection = new SqlConnection(_sqlConnString))
            {
                conection.Open();

                var detail = conection.Query<Report>(sql, p, commandType: CommandType.StoredProcedure);

                return detail.ToList();
            }


        }

        public  List<Status> StatusResponsLUT()
        {
            string sql = "pStatusResponsLUT_GetAll";

            var p = new DynamicParameters();



            using (var connection = new SqlConnection(_sqlConnString))
            {
                connection.Open();
                var result = connection.Query<Status>(sql, p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public  Request RequestGetOne(int RequestId)
        {
            string sql = "pRequestGetOne";

            var p = new DynamicParameters();
            p.Add("RequestId", RequestId, dbType: DbType.Int32);


            using (var conection = new SqlConnection(_sqlConnString))
            {
                conection.Open();

                var detail = conection.QueryFirstOrDefault<Request>(sql, p, commandType: CommandType.StoredProcedure);

                return detail;
            }


        }

        public List<ResponseDetail> ReportResponse(int RequestId)
        {
            string sql = "pReportResponse";

            var p = new DynamicParameters();
            p.Add("RequestId", RequestId, dbType: DbType.Int32);


            using (var conection = new SqlConnection(_sqlConnString))
            {
                conection.Open();

                var detail = conection.Query<ResponseDetail>(sql, p, commandType: CommandType.StoredProcedure);

                return detail.ToList();
            }


        }
    }
}
