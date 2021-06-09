using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAddressApi.Model
{
    public class User
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
