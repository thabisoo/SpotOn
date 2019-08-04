using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Entities.Users
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}
