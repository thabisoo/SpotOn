using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
