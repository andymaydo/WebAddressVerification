using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressApi.Model
{
    class ReportRequest
    {
        public int Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CallerId { get; set; }
        public string Street { get; set; }


     
    }
}
