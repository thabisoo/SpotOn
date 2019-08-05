using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpotOn.ApplicationLogic.ViewModels.Users
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Please provide a username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [MinLength(6, ErrorMessage = "Password too short")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }
    }
}
