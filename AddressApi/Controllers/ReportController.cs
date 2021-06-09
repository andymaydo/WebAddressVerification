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

namespace AddressApi.Controllers
{
    [Route("v1/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IWebAddressVerification _webAddressVerification;
        private ILogger<ReportController> _logger { get; set; }
        public ReportController(IWebAddressVerification web, ILogger<ReportController> logger)
        {
            _logger = logger;
            _webAddressVerification = web;
        }

        // GET: v1/Films/
        [HttpPost]
        
        public ActionResult<List<Report>> PostReport([FromBody]ReportPost report)
        {
            try
            {
                var ReportList = _webAddressVerification.ReportRequestGet(report.Status, report.FromDate, report.ToDate, report.CallerId, report.Street);
                if (ReportList?.Count > 0)
                {
                    return Ok(ReportList);
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
        [HttpGet]
        public ActionResult<Request> RequestGetOne(int RequestId)
        {
            try
            {
                var Request = _webAddressVerification.RequestGetOne(RequestId);
                if (Request != null)
                {
                    return Ok(Request);
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


        [HttpGet]
        public ActionResult<List<ResponseDetail>> ReportResponse(int RequestId)
        {
            try
            {
                var ResponseList = _webAddressVerification.ReportResponse(RequestId);
                var Request = _webAddressVerification.RequestGetOne(RequestId);
                if (Request != null)
                {
                    return Ok(ResponseList);
                }
                return NotFound();


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }
        //[HttpGet]
        //public ActionResult<RequestResponse> RequestResponse(int RequestId)
        //{
        //    try
        //    {
        //        RequestResponse requestResponse = new RequestResponse();
        //        requestResponse .ResponseDetails= _webAddressVerification.ReportResponse(RequestId);
        //         requestResponse.Request = _webAddressVerification.RequestGetOne(RequestId);
        //        if (requestResponse.Request != null)
        //        {
        //            return Ok(requestResponse);
        //        }
        //        return NotFound();


        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message, e.StackTrace);
        //        return StatusCode(500);
        //    }
        //}
    }
}

