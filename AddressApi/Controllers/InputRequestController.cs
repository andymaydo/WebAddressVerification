using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressApi.Model;
using Aplication;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace AddressApi.Controllers
{
    
    [Route("v1/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class InputRequestController : ControllerBase
    {
        public RequestValid RequestValid { get; set; }
        private IWebAddressVerification _webAddressVerification;
        private ILogger<InputRequestController> _logger { get; set; }
        public InputRequestController(IWebAddressVerification web, ILogger<InputRequestController> logger)
        {
            _logger = logger;
            _webAddressVerification = web;
            RequestValid = new RequestValid();
        }

        // GET: api/Films
        [HttpGet]
        public ActionResult<List<CountryCode>> GetAll()
        {
            try
            {
                var CountrycodeList = _webAddressVerification.CountryCodeGetAll();
                if (CountrycodeList?.Count > 0)
                {
                    return Ok(CountrycodeList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost]

        public ActionResult<RequestValid> RequestValidate(Request request)
        {
            try
            {
                (RequestValid.Status,RequestValid.Details) = _webAddressVerification.RequestValidate(request);
               
                    return Ok(RequestValid);
               
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
