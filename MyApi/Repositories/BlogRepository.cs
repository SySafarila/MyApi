using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Exceptions;
using MyApi.Models;

namespace MyApi.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync(string? searchTitle, string? sort)
        {
            var query = _context.Blogs.AsQueryable();

            if (searchTitle != null)
            {
                query = query.Where(blog => blog.title.Contains(searchTitle));
            }

            switch (sort)
            {
                case "asc":
                    query = query.OrderBy(blog => blog.id);
                    break;
                case "desc":
                    query = query.OrderByDescending(blog => blog.id);
                    break;
                default:
                    query = query.OrderByDescending(blog => blog.id);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.Include(b => b.comments).FirstOrDefaultAsync(b => b.id == id);
            if (blog == null)
            {
                throw new HttpException("Blog not found", 404);
            }
            return blog;
        }

        public async Task<Blog> AddAsync(BlogRequestDto req)
        {
            var blog = new Blog
            {
                title = req.title,
                description = req.description,
                content = req.content,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return blog;
        }

        public async Task<Blog> DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog == null)
            {
                throw new HttpException("Blog not found", 404);
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
