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
            try
            {
                var blog = await _blogService.GetByIdAsync(id);
                return blog;
            } catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAll([FromQuery] string? searchTitle, [FromQuery] string? sort = "asc")
        {
            var blogs = await _blogService.GetAllAsync(searchTitle?.ToLower(), sort?.ToLower());

            return Ok(blogs);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> Store(BlogRequest req)
        {
            try
            {
                await _blogService.AddAsync(req);

                return Ok(new { message = "Blog created" });
            } catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _blogService.DeleteAsync(id);
                return Ok(new { message = "Blog deleted" });
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BlogRequest req)
        {
            try
            {
                await _blogService.UpdateAsync(id, req);

                return Ok(new { message = "Blog updated" });
            } catch
            {
                return BadRequest();
            }
        }
    }
}
