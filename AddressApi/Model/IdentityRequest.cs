using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressApi.Model
{
    public class IdentityRequest
    {
       
        [Required]
        public string Username { get; set; }

        
        [Required]
        public string Password { get; set; }

        
        [Required]
        [Range(1, 720, ErrorMessage = "TOKENDURATION OUT OF RANGE (1-720)")]
        public int TokenDuration { get; set; }
    }
}
