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

        public async Task<BlogDetailDto> AddAsync(BlogRequestDto req)
        {
            var blog = await _blogRepository.AddAsync(req);
            return blog;
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

        public async Task<BlogDetailDto> UpdateAsync(int id, BlogRequestDto req)
        {
            return await _blogRepository.UpdateAsync(id, req);
        }
    }
}
