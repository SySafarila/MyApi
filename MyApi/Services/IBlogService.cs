using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort);
        Task<BlogDetailDto> GetByIdAsync(int id);
        Task AddAsync(BlogRequestDto req);
        Task UpdateAsync(int id, BlogRequestDto req);
        Task DeleteAsync(int id);
    }
}
