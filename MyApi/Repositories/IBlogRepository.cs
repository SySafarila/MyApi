using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync(string? searchTitle, string? sort);
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> AddAsync(BlogRequestDto request);
        Task<Blog> UpdateAsync(Blog blog);
        Task DeleteAsync(int id);
    }
}
