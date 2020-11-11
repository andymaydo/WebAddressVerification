using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAddressVerification.Model
{
    public static class DAL
    {
        public static string _sqlConnString;

        public static List<CountryCode> CountryCodeGetAll()
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
        public static  (int Status,  List<ResponseDetail> Detail) RequestValidate( Request a)
            {
            string sql = "pValidate_Request";

            var p = new DynamicParameters();
            p.Add("CountryCodeISO2", a.CountryCodeISO2);
            p.Add("State ", a.State);
            p.Add("City ", a.City);
            p.Add("Zip",a.Zip);
            p.Add("Street", a.Street);
            p.Add("StreetNumber", a.Number);
            p.Add("CallerId", a.CallerId);
            p.Add("RequestId", a.RequestId, direction:ParameterDirection.Output);
            p.Add("ResponseStatus",dbType: DbType.Int32,  direction: ParameterDirection.Output);

            using (var conection=new SqlConnection(_sqlConnString))
            {
                conection.Open();
             
                var detail = conection.Query<ResponseDetail>(sql, p, commandType: CommandType.StoredProcedure);
                int status = p.Get<int>("ResponseStatus");
                return (status, detail.ToList());
            }


        }
        public static void RequestResult (Request b)
        {

        }
    }
}
