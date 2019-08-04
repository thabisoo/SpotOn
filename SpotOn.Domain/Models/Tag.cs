using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.Domain.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
