using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aplication.Models;
using Domain;

namespace Aplication
{
    public interface IIdentity
    {
        UserAuthInfo LoginAsync(string email, string password, int tokenDuration);
    }

}
