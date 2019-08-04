using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpotOn.Domain.Models
{
    public class PostTag
    {
        public Guid TagId { get; set; }

        public Guid PostId { get; set; }

        [Required]
        public virtual Tag Tag { get; set; }

        [Required]
        public virtual Post Post { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
