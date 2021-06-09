using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Headers;
using Domain;
using Aplication;

namespace WebAddressVerification.Pages
{
    public class ResponseDetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [Required]
        public int Rid { get; set; }

        public string ReturnUrl { get; set; }
        public InputModel Input { get; set; }
    
        public class InputModel
        {
            public Request CurrentRequest { get; set; }

            public List<ResponseDetail> CurrentResponseDetail { get; set; }
            public string ErrorMesege { get; set; }
            public string ErrorExist { get; set; }
            //public RequestResponse RequestResponse { get; set; }

        }
        private IWebAddressVerification _webAddressVerification;
        
        public ResponseDetailModel( IWebAddressVerification webAddressVerification)
        {
            _webAddressVerification = webAddressVerification;
            Input = new InputModel();
            

            //CurrentRequest = new Request();
            //CurrentResponseDetail = new List<ResponseDetail>();

        }

        //public IActionResult OnGet()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Input.ErrorMesege = "Invalid Date for RequestIs";
        //        return Page();
        //    }
        //    else
        //    {
        //        try
        //        {
        //            Input.CurrentRequest = _webAddressVerification.RequestGetOne(Rid);

        //        }
        //        catch (Exception ex)
        //        {
        //            Input.ErrorMesege = ex.Message;

        //        }

        //        try
        //        {
        //            Input.CurrentResponseDetail = _webAddressVerification.ReportResponse(Rid);
        //        }
        //        catch (Exception e)
        //        {
        //            Input.ErrorMesege = e.Message;
        //        }
        //        return Page();
        //    }

        //    //ReturnUrl = Request.Headers["Referer"].ToString();
        //    //return Page();

        //}
        //public IActionResult OnPost()
        //{
        //    ReturnUrl = Request.Headers["Referer"].ToString();
        //    ReturnUrl = ReturnUrl ?? Url.Content("~/");     

        //    return LocalRedirect(ReturnUrl);

        //}
        public IActionResult OnGetPartial(int Rid)
        {

            //try
            //{
            //    //Input.CurrentRequest = _webAddressVerification.RequestGetOne(Rid);
            //Input.RequestResponse = _webAddressVerification.RequestResponse(Rid);

            //}
            //catch (Exception ex)
            //{
            //Input.ErrorMesege = ex.Message;

            //}


            try
            {
                Input.CurrentRequest = _webAddressVerification.RequestGetOne(Rid);

            }
            catch (Exception ex)
            {
                Input.ErrorMesege = ex.Message;

            }
            try
            {
                Input.CurrentResponseDetail = _webAddressVerification.ReportResponse(Rid);
            }
            catch (Exception e)
            {
                Input.ErrorMesege = e.Message;
            }
            return Partial("_ResponseDetail",Input);
            
        }
    } 
}
