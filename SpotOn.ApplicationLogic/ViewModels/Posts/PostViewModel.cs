using Microsoft.AspNetCore.Http;
using SpotOn.ApplicationLogic.ViewModels.Users;
using SpotOn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpotOn.ApplicationLogic.ViewModels.Posts
{
    public class PostViewModel
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter post title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter post body")]
        public string Body { get; set; }

        public Guid UserId { get; set; }

        public UserViewModel Author { get; set; }

        public DateTimeOffset Date { get; set; }

        public int PostsInPage { get; set; }

        public string Blurb {
            get { return StringExtensions.Truncate(Body, 300); }
            set {}
        }

        public string TagsList { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}