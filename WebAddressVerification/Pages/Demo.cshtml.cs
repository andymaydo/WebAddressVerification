using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            public string State  { get; set; }

            [Display(Name = "City")]
            [Required(ErrorMessage = "This  field is required")]
            public string City { get; set; }

            [Display(Name = "Zipp")]
            [Required(ErrorMessage = "This  field is required")]
            public int Zip { get; set; }

            [Display(Name = "Street")]
            [Required(ErrorMessage = "This  field is required")]
            public string Street { get; set; }
            
            [Display(Name = "Number")]
            [Required(ErrorMessage = "This  field is required")]
            [RegularExpression("([1-9][0-9]*)",ErrorMessage ="must be a number")]
            public string Number { get; set; }
          
        }
       public List<SelectListItem> CountrySelectItems { get; set; }



        public DemoModel()
        {
            Input = new AddAdress();

            CountrySelectItems = new List<SelectListItem>();            
            foreach (var a in Constants.CountryCodeISO2)
            {                
                CountrySelectItems.Add(new SelectListItem() {Value = a.Key, Text = a.Value });            
            }
            CountrySelectItems.Insert(0, new SelectListItem() { Value = "", Text = "select a country" });



        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
          

            return Page();
            }
            return Page();
        }
    }
}
