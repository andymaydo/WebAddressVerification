using Aplication;
using Aplication.Models;

using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class IdentityService : IIdentity
    {

        private readonly string _jwtSettings;
       
      
        private IWebAddressVerification _webaddressService;
        

        public IdentityService(IWebAddressVerification webAddressVerification, IConfiguration configuration)
        {
            _webaddressService = webAddressVerification;
            _jwtSettings = configuration["JwtSecret"];
           
        }

        public  UserAuthInfo LoginAsync(string username, string password, int tokenDuration)
        {
            UserAuthInfo _UserAuthInfo=new UserAuthInfo();

            

          

            _UserAuthInfo.UserId = _webaddressService.FindUser(username,password);

            if (_UserAuthInfo.UserId == 0)
            {
                _UserAuthInfo.authInfo.Success = false;
                _UserAuthInfo.authInfo.AuthErrDescr = "User not found";
              

                return _UserAuthInfo;
            }

           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, username),
                    new Claim("userid",_UserAuthInfo.UserId.ToString() )



                }),
                Expires = DateTime.UtcNow.AddHours(tokenDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            _UserAuthInfo.authInfo.Success = true;
            _UserAuthInfo.authInfo.Token = tokenHandler.WriteToken(token);

           

            return _UserAuthInfo;



        }


    }
}

