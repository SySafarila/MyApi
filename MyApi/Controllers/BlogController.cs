using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailDto>> GetById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            return blog;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAll([FromQuery] string? searchTitle, [FromQuery] string? sort = "asc")
        {
            var blogs = await _blogService.GetAllAsync(searchTitle?.ToLower(), sort?.ToLower());

            return Ok(blogs);
        }

        [HttpPost]
        public async Task<ActionResult<BlogDetailDto>> Store(BlogRequestDto req)
        {
            var blog = await _blogService.AddAsync(req);

            return Ok(blog);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return Ok(new { message = "Blog deleted" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogDetailDto>> Update(int id, BlogRequestDto req)
        {
            var blog = await _blogService.UpdateAsync(id, req);

            return Ok(blog);
        }
    }
}
