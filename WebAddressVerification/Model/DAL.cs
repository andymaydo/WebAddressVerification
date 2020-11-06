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

        public static List<CountryCode> CountryCode_GetAll()
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
    }
}
