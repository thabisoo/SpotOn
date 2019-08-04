using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpotOn.ApplicationLogic.ViewModels.Users
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Please provide your email")]
        public string Email {get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}
