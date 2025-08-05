using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Requests;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetById(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return blog;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAll([FromQuery] string? searchTitle, [FromQuery] string? sort = "asc")
        {
            var query = _context.Blogs.AsQueryable();

            if (sort?.ToLower() == "desc")
            {
                query = query.OrderByDescending(blog => blog.id);
            }
            else
            {
                query = query.OrderBy(blog => blog.id);
            }

            if (searchTitle != null)
            {
                query = query.Where(blog => blog.title.Contains(searchTitle));
            }

            var blogs = await query.ToListAsync();

            return Ok(blogs);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> Store(BlogRequest req)
        {
            var blog = new Blog
            {
                title = req.title,
                content = req.content,
                description = req.description,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BlogRequest req)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog == null)
            {
                return NotFound();
            }

            blog.title = req.title;
            blog.content = req.content;
            blog.description = req.description;
            blog.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(blog);
        }
    }
}
