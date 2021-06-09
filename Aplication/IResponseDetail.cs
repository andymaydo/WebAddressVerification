using Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aplication
{
    public interface IResponseDetail
    {
       
            public List<ResponseDetail> ReportResponse(int RequestId);
            public (int Status, List<ResponseDetail> Detail) RequestValidate(Request a);
    }
}
