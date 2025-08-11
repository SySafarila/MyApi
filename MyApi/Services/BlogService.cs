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

        public async Task<Blog> AddAsync(BlogRequestDto req)
        {
            return await _blogRepository.AddAsync(req);
        }

        public async Task<Blog> DeleteAsync(int id)
        {
            return await _blogRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetAllAsync(string? searchTitle, string? sort)
        {
            return await _blogRepository.GetAllAsync(searchTitle, sort);
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            return await _blogRepository.UpdateAsync(blog);
        }
    }
}
