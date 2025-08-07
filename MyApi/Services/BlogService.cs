using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task AddAsync(BlogRequestDto req)
        {
            await _blogRepository.AddAsync(req);
        }

        public async Task DeleteAsync(int id)
        {
            await _blogRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort)
        {
            return await _blogRepository.GetAllAsync(searchTitle, sort);
        }

        public async Task<BlogDetailDto> GetByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, BlogRequestDto req)
        {
            await _blogRepository.UpdateAsync(id, req);
        }
    }
}
