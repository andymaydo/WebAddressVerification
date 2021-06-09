using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressApi.Model
{
    public class ReportPost
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        public int CallerId { get; set; }
        public string Street { get; set; }

        public int Status { get; set; }
      
    }
}
