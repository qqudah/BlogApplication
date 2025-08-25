using BlogApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Blog> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
