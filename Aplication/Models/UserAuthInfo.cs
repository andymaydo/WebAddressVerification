using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Aplication.Models
{
    public class UserAuthInfo
    {
        public AuthInfo authInfo { get; set; }
        public int UserId { get; set; }

        public UserAuthInfo()
        {
            authInfo = new AuthInfo();
        }

    }

    
    public class AuthInfo
    {


        public bool Success { get; set; }



        public string Token { get; set; }



        public int AuthErrCode { get; set; }



        public string AuthErrDescr { get; set; }


    }
}
