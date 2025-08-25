using BlogApplication.Data;
using BlogApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDBContext _blogDBContext;
        public BlogRepository(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }
        public async Task<Blog> AddAsync(Blog blog)
        {   await _blogDBContext.BlogPosts.AddAsync(blog);
            await _blogDBContext.SaveChangesAsync();
           return blog;
        }

        public async Task<Blog?> DeleteAsync(Guid id)
        {
           var blog = await _blogDBContext.BlogPosts.FindAsync(id);
            if(blog != null)
            {
                _blogDBContext.BlogPosts.Remove(blog);
                await _blogDBContext.SaveChangesAsync();
                return blog;
            }
            return null;

        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _blogDBContext.BlogPosts
                                       .Include(b => b.Tags)
                                       .ToListAsync();
        }

        public async Task<Blog?> GetAsync(Guid id)
        {
            return await _blogDBContext.BlogPosts
                                       .Include(b => b.Tags)
                                       .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task<Blog?> UpdateAsync(Blog blog)
        {
            
            var existingBlog = await _blogDBContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blog.Id);

            if (existingBlog != null)
            {
               
                existingBlog.Heading = blog.Heading;
                existingBlog.PageTitle = blog.PageTitle;
                existingBlog.Content = blog.Content;
                existingBlog.Author = blog.Author;
                existingBlog.Visable = blog.Visable;
                existingBlog.ImageURL = blog.ImageURL;

                
                existingBlog.Tags.Clear();
                foreach (var tag in blog.Tags)
                {
                    existingBlog.Tags.Add(tag);
                }

                await _blogDBContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

    }
}
