using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication
{
     public interface IWebAddressVerification
    {
        public List<CountryCode> CountryCodeGetAll() { throw new NotImplementedException(); }
        public List<Report> ReportRequestGet(int? Status, DateTime FromDate, DateTime ToDate, int CallerId, string Street) { throw new NotImplementedException(); }
        public Request RequestGetOne(int RequestId) { throw new NotImplementedException(); }
        public List<ResponseDetail> ReportResponse(int RequestId) { throw new NotImplementedException(); }
        public RequestResponse RequestResponse(int RequestId) { throw new NotImplementedException(); }
        public (int Status, List<ResponseDetail> Detail) RequestValidate(Request a){ throw new NotImplementedException(); }
       
        
        public List<Status> StatusResponsLUT() { throw new NotImplementedException(); }
        public  int FindUser(string password,string username) { return 1; }
    }

}
