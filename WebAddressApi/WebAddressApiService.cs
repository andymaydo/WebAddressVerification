using Aplication;
using Aplication.Models;
using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebAddressApi.Model;

using WebAddressApi.Models;

namespace WebAddressApi
{
    class WebAddressApiService:IWebAddressVerification
    {
        private string _apiBasestring;
        private readonly HttpClient _client;
        private string _sqlConnString;
        private readonly IHttpClientFactory _clientFactory;
        public UserAuthInfo AuthInfo { get; set; }
        public User User1 { get; set; }
        public ReportRequest reportRequest { get; set; }
        public WebAddressApiService(IConfiguration configuration,HttpClient client,IHttpClientFactory clientFactory)
        {
            _apiBasestring = configuration["AddressApiUrl"];
            _client = client;
            _sqlConnString = configuration["ConnString"];
            _clientFactory = clientFactory;
            reportRequest = new ReportRequest();
            AuthInfo = new UserAuthInfo();
            User1 = new User();
        }
        private UserAuthInfo UserLoginRequest(User user)
        {
            user.Username = "k";
            user.Password = "kk";
            user.TokenDuration = 1;

            var createdTask = new UserAuthInfo();

            string controllerurl = "/v1/Identity";

            string url = _apiBasestring + controllerurl;
            var content = JsonConvert.SerializeObject(user);
            using (var client = _clientFactory.CreateClient())
            {
                var httpResponse = client.PostAsync(url, new StringContent(content, Encoding.Default, "application/json")).Result;

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot add a todo task");
                }
                createdTask = JsonConvert.DeserializeObject<UserAuthInfo>(httpResponse.Content.ReadAsStringAsync().Result);
                Constants.ApiToken = createdTask.authInfo.Token;
            }


            return createdTask;
        }
        //public List<CountryCode> CountryCodeGetAll()

        //{
        //    string url = _apiBasestring + "/v1/InputRequest";
        //    var httpResponse =  _client.GetAsync(url).Result;

        //    if (!httpResponse.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Cannot retrieve tasks");
        //    }

        //    var content = httpResponse.Content.ReadAsStringAsync().Result;
        //    var tasks = JsonConvert.DeserializeObject<List<CountryCode>>(content);

        //    return tasks;
        //}
        public List<CountryCode> CountryCodeGetAll()

        {
            var tasks = new List<CountryCode>() ;

            string url = _apiBasestring + "/v1/InputRequest";
            using(var client = _clientFactory.CreateClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Constants.ApiToken);
                   var httpResponse = client.GetAsync(url).Result;
                if (!httpResponse.IsSuccessStatusCode&& "Unauthorized" == httpResponse.StatusCode.ToString())
                {
                   
                    AuthInfo = UserLoginRequest(User1);
                    tasks = CountryCodeGetAll();
                }
                else
                {
                   var content = httpResponse.Content.ReadAsStringAsync().Result;
                    tasks = JsonConvert.DeserializeObject<List<CountryCode>>(content);

                }
            }

            return tasks;
        }

        public List<Report> ReportRequestGet(int? Status,DateTime FromDate,DateTime ToDate,int CallerId,string Street)
        {
            reportRequest.Status = (int)Status;
            reportRequest.FromDate = FromDate;
            reportRequest.ToDate = ToDate;
            reportRequest.CallerId = CallerId;
            reportRequest.Street = Street;
            string url = _apiBasestring + "/v1/Report/PostReport";
            var content = JsonConvert.SerializeObject(reportRequest);
            var httpResponse = _client.PostAsync(url, new StringContent(content, Encoding.Default, "application/json")).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot add a todo task");
            }

            var createdTask = JsonConvert.DeserializeObject<List<Report>>( httpResponse.Content.ReadAsStringAsync().Result);
            return createdTask;
        }


        //public (int Status, List<ResponseDetail> Detail) RequestValidate(Request request)
        //{

        //    string url = _apiBasestring + "/v1/InputRequest";

        //    var content = JsonConvert.SerializeObject(request);
        //    var reqContent = new StringContent(content, Encoding.Default, "application/json");
        //    var httpResponse = _client.PostAsync(url, reqContent).Result;
        //    //var httpResponse = _client.PostAsync(url, content).Result;

        //    if (!httpResponse.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Cannot add a todo task");
        //    }

        //    var respContent = httpResponse.Content.ReadAsStringAsync().Result;
        //    var createdTask = JsonConvert.DeserializeObject<RequestValid>(respContent);
        //    return (createdTask.Status,createdTask.Details);
        //}
        public (int Status, List<ResponseDetail> Detail) RequestValidate(Request request)
        {
            var requestvalid = new RequestValid();
            string url = _apiBasestring + "/v1/InputRequest";
            using(var client = _clientFactory.CreateClient())
            {
                var content = JsonConvert.SerializeObject(request);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Constants.ApiToken);
                var reqContent = new StringContent(content, Encoding.Default, "application/json");
                var httpResponse = client.PostAsync(url, reqContent).Result;
                if (!httpResponse.IsSuccessStatusCode && "Unauthorized" == httpResponse.StatusCode.ToString())
                {

                    AuthInfo = UserLoginRequest(User1);
                    (requestvalid.Status, requestvalid.Details) = RequestValidate(request);
                }
                else
                {
                    var respContent = httpResponse.Content.ReadAsStringAsync().Result;
                    requestvalid = JsonConvert.DeserializeObject<RequestValid>(respContent);
                }
            }
           
           
 

          
            return (requestvalid.Status, requestvalid.Details);
        }

        public Request RequestGetOne(int RequestId)

        {
            string controllerurl = "/v1/Report";
            string actionurl = "/RequestGetOne";
            string propertyurl = "?RequestId=" + RequestId.ToString();
            string url = _apiBasestring + controllerurl+ actionurl+propertyurl;

            var httpResponse = _client.GetAsync(url).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = httpResponse.Content.ReadAsStringAsync().Result;
            var tasks = JsonConvert.DeserializeObject<Request>(content);

            return tasks;
        }

        public List<ResponseDetail> ReportResponse(int RequestId)


        {
            string controllerurl = "/v1/Report";
            string actionurl = "/ReportResponse";
            string propertyurl = "?RequestId=" + RequestId.ToString();
            string url = _apiBasestring + controllerurl+ actionurl+ propertyurl;
            var httpResponse = _client.GetAsync(url).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = httpResponse.Content.ReadAsStringAsync().Result;
            var tasks = JsonConvert.DeserializeObject<List<ResponseDetail>>(content);

            return tasks;
        }

        public RequestResponse RequestResponse(int RequestId)


        {
            string controllerurl = "/v1/Report";
            string actionurl = "/RequestResponse";
            string propertyurl = "?RequestId=" + RequestId.ToString();
            string url = _apiBasestring + controllerurl + actionurl + propertyurl;
            var httpResponse = _client.GetAsync(url).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = httpResponse.Content.ReadAsStringAsync().Result;
            var tasks = JsonConvert.DeserializeObject<RequestResponse>(content);

            return tasks;
        }

        public List<Status> StatusResponsLUT()
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
    }
}
