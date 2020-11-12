using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WebAddressVerification.Model;
namespace WebAddressVerification.Pages
 
{

    public class DemoModel : PageModel
    {
        [BindProperty]
        public AddAdress Input { get; set; }

        public class AddAdress
        {
            [Display(Name = "Country")]
            [Required(ErrorMessage = "This  field is required")]
            public string Country { get; set; }

            [Display(Name = "Staat")]
            [Required(ErrorMessage = "This  field is required")]
            public string State { get; set; }

            [Display(Name = "City")]
            [Required(ErrorMessage = "This  field is required")]
            public string City { get; set; }

            [Display(Name = "Zipp")]
            [Required(ErrorMessage = "This  field is required")]
            public string Zip { get; set; }

            [Display(Name = "Street")]
            [Required(ErrorMessage = "This  field is required")]
            public string Street { get; set; }

            [Display(Name = "Number")]
            [Required(ErrorMessage = "This  field is required")]
            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "must be a number")]
            public string Number { get; set; }

        }
        public List<SelectListItem> CountrySelectItems { get; set; }

        public string SuccessMessage {get; set;}

        public string ErrorMessage { get; set; }
       
        public int ReqId { get; set; }

        public int ResponseResult { get; set; } = -1;
        public List<ResponseDetail> ResponseDetails { get; set; }


        public DemoModel()
        {
            Input = new AddAdress();
           
            CountrySelectItems = new List<SelectListItem>();
            ResponseDetails = new List<ResponseDetail>();
            try
            {
                var CountryCodes = DAL.CountryCodeGetAll();

                foreach (var a in CountryCodes)
                {
                    CountrySelectItems.Add(new SelectListItem() { Value = a.Iso2Code, Text = a.CountryName });
                }
                CountrySelectItems.Insert(0, new SelectListItem() { Value = "", Text = "select a country" });

                
            }
            catch(Exception e)
            {
               ErrorMessage = e.Message;
               

            }



        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
          

            return Page();
            }
            try
            {
                Request RequestInput = new Request();
                RequestInput.CountryCodeISO2 = Input.Country;
                RequestInput.State = Input.State;
                RequestInput.City = Input.City;
                RequestInput.Street = Input.Street;
                RequestInput.Zip = Input.Zip;
                RequestInput.Number = Input.Number;
                RequestInput.CallerId = 0;

                (ResponseResult, ResponseDetails) = DAL.RequestValidate(RequestInput);

                SuccessMessage = "Your address is availabel";
          
            }
            catch(Exception e ) 
            {
                ErrorMessage = e.Message;
               
               
            }
         
            return Page();
        }
    }
}
