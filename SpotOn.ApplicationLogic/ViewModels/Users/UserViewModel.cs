using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.ViewModels.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}