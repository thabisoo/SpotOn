using Microsoft.EntityFrameworkCore;
using SpotOn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostTag>().HasKey(x => new { x.TagId, x.PostId });
        }

    }
}
