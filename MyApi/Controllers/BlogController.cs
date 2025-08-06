using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Requests;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        public BlogController(AppDbContext context, IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            return blog;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAll([FromQuery] string? searchTitle, [FromQuery] string? sort = "asc")
        {
            var blogs = await _blogService.GetAllAsync();

            return Ok(blogs);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> Store(BlogRequest req)
        {
            await _blogService.AddAsync(req);

            return Ok(new { message = "Blog created" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return Ok(new { message = "Blog deleted" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BlogRequest req)
        {
            await _blogService.UpdateAsync(id, req);

            return Ok(new { message = "Blog updated" });
        }
    }
}
