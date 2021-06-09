using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication
{
    public interface IRequest
    {
        public Request RequestGetOne(int RequestId);
    }
}
