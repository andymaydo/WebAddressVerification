using AddressApi.Model;
using Aplication;
using Aplication.Models;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressApi.Controllers
{
    [Route("v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentity _identityService;

        public IdentityController(IIdentity identityService)
        {
            _identityService = identityService;

        }

        [HttpPost]
        public ActionResult<UserAuthInfo> Login([FromBody] IdentityRequest request)
        {

            UserAuthInfo _UserAuthInfo =  _identityService.LoginAsync(request.Username, request.Password,  request.TokenDuration);

            if (!_UserAuthInfo.authInfo.Success)
            {
                //return BadRequest(_UserAuthInfo.authInfo);
                return NotFound(_UserAuthInfo);
            }

            return Ok(_UserAuthInfo);
        }


    }
}
