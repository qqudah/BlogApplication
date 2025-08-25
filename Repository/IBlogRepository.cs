using BlogApplication.Models.Domain;
namespace BlogApplication.Repository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog?> GetAsync(Guid id);
        Task<Blog> AddAsync(Blog blog);
        Task<Blog?> UpdateAsync(Blog blog);
        Task<Blog?> DeleteAsync(Guid id);
    }
}
