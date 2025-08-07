using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort);
        Task<BlogDetailDto> GetByIdAsync(int id);
        Task<BlogDetailDto> AddAsync(BlogRequestDto request);
        Task<BlogDetailDto> UpdateAsync(int id, BlogRequestDto request);
        Task DeleteAsync(int id);
    }
}
