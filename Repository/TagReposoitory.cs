using BlogApplication.Data;
using BlogApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Repository
{
    public class TagRepository : ITagInterface
    {
        private readonly BlogDBContext _blogDBContext;

        public TagRepository(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await _blogDBContext.Tags.AddAsync(tag);
            await _blogDBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await _blogDBContext.Tags.FindAsync(id);
            if (tag == null) return null;

            _blogDBContext.Tags.Remove(tag);
            await _blogDBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _blogDBContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _blogDBContext.Tags.FindAsync(id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _blogDBContext.Tags.FindAsync(tag.Id);
            if (existingTag == null) return null;

            existingTag.Name = tag.Name;
            await _blogDBContext.SaveChangesAsync();
            return existingTag;
        }
    }
}
