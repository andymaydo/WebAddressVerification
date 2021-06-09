using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressApi.Model
{
    public class RequestValid
    {
        public int Status { get; set; }
        public List<ResponseDetail> Details { get; set; }
    }
}
