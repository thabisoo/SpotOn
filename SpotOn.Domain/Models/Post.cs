using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string ImagePath { get; set; }

        public virtual User User { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
