using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Request
    {
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; } 
        public int CallerId { get; set; }
     
        public string CountryCodeISO2 { get; set; }

      
        public string State { get; set; }

    
        public string City { get; set; }

       
        public string Zip { get; set; }

     
        public string Street { get; set; }

       
        public string StreetNumber { get; set; }
    }
}
