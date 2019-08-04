using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Entities.Tags
{
    public class TagEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
