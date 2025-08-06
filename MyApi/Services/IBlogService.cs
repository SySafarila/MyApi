using MyApi.Models;
using MyApi.Requests;

namespace MyApi.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task AddAsync(BlogRequest req);
        Task UpdateAsync(int id, BlogRequest req);
        Task DeleteAsync(int id);
    }
}
