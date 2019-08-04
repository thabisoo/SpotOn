using Microsoft.AspNetCore.Http;
using SpotOn.ApplicationLogic.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Entities.Posts
{
    public class PostEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Guid UserId { get; set; }

        public UserEntity Author { get; set; }

        public DateTimeOffset Date { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
