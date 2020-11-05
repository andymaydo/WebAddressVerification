using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAddressVerification.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string _name;

        
        [BindProperty]
        [Display(Name = "Student Name")]
        [Required(ErrorMessage ="This  field is required")]
        public string Sname { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            
        }

        public IActionResult OnPost()
        {
            
            return Page();
        }
    }
}
