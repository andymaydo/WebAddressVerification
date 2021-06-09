using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RequestResponse
    {
        public Request Request { get; set; }
        public List<ResponseDetail> ResponseDetails { get; set; }
    }
}
