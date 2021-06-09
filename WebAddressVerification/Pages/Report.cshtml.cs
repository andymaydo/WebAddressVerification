using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAddressVerification.Pages
{
    public class ReportModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public InputModel Input { get; set; }
        public List<Report> RequestReport { get; set; }
        public string ErrorMessage { get; set; }

        public List<SelectListItem> StatusSelectItems { get; set; }
        public bool ReportStarted { get; set; } = false;
        public class InputModel
        {
            public int Status { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public int CallerId { get; set; }
            public string Street { get; set; }
        }
        private IWebAddressVerification _webAddressVerification;
        public ReportModel(IWebAddressVerification webAddress)
        {

            _webAddressVerification = webAddress;
            RequestReport = new List<Report>();
            StatusSelectItems = new List<SelectListItem>();


            var statuses = _webAddressVerification.StatusResponsLUT();

            foreach (var a in statuses)
            {
               StatusSelectItems.Add(new SelectListItem() { Value = a.StatusId.ToString(), Text = a.StatusName });
            }
            StatusSelectItems.Insert(0, new SelectListItem() { Value = "-1", Text = "All Status" });
        }
        public void OnGet()
        {
            Input = new InputModel();
            Input.FromDate = DateTime.UtcNow.AddDays(-1);
            Input.ToDate = DateTime.UtcNow;
            Input.CallerId = 0;
        }

        //public IActionResult OnGetList()
        //{
        //    if (!ModelState.IsValid)
        //    {

        //        return Page();
        //    }

            
        //    RequestReport = _webAddressVerification.ReportRequestGet(Input.Status, Input.FromDate, Input.ToDate, Input.CallerId, Input.Street);
        //    ReportStarted = true;

        //    return Page();
        //}
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {


                return Page();
            }
            try
            {
                RequestReport = _webAddressVerification.ReportRequestGet(Input.Status, Input.FromDate, Input.ToDate, Input.CallerId, Input.Street);
                ReportStarted = true;
            }
            catch
            {
                ReportStarted = true;
                ErrorMessage = "Report not found";
            }

            return Page();
        }
    }
}
