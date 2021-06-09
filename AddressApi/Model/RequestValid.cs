using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressApi.Model
{
    public class RequestValid
    {
        public int Status { get; set; }
        public List<ResponseDetail> Details { get; set; }
    }
}
