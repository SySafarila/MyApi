using MyApi.Models;
using MyApi.Requests;

namespace MyApi.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync(string? searchTitle, string? sort);
        Task<Blog> GetByIdAsync(int id);
        Task AddAsync(BlogRequest request);
        Task UpdateAsync(int id, BlogRequest request);
        Task DeleteAsync(int id);
    }
}
