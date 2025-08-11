using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync(string? searchTitle, string? sort);
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> AddAsync(BlogRequestDto req);
        Task<Blog> UpdateAsync(Blog blog);
        Task<Blog> DeleteAsync(int id);
    }
}
