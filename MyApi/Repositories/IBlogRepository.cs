using MyApi.DTOs;
using MyApi.Models;
using MyApi.Requests;

namespace MyApi.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort);
        Task<BlogDetailDto> GetByIdAsync(int id);
        Task AddAsync(BlogRequest request);
        Task UpdateAsync(int id, BlogRequest request);
        Task DeleteAsync(int id);
    }
}
