using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAddressVerification.Model
{
    public class ResponseDetail
    {
        public int RequestId { get; set; }
       
      
        public string CountryCodeISO2 { get; set; }


        public string State { get; set; }


        public string City { get; set; }


        public string Zip { get; set; }


        public string Street { get; set; }


        public string StreetNumber { get; set; }

        public int Rate { get; set; }

    }
}

