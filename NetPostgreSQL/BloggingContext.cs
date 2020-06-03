using Microsoft.EntityFrameworkCore;
using NetPostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPostgreSQL
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().Property(p => p.BlogId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>()
                .HasData(
                new Blog()
                {
                    BlogId = 1,
                    Url= "https://atomix.vg"
                });

            modelBuilder.Entity<Post>().Property(p => p.PostId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().HasData(
                new Post()
                {
                    PostId = 1,
                    Title = "The Last Of Us Part II Review",
                    Content = "This game is awesome!",
                    BlogId = 1
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
