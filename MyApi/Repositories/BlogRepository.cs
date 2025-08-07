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

        public async Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort)
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

            var result = query.Select(b => new BlogDto
            {
                id = b.id,
                description = b.description,
                title = b.title,
                created_at = b.created_at,
                updated_at = b.updated_at,
                views = b.views
            });

            return await result.ToListAsync();
        }

        public async Task<BlogDetailDto> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.Include(b => b.comments).FirstOrDefaultAsync(b => b.id == id);
            if (blog == null)
            {
                throw new HttpException("Blog not found", 404);
            }
            blog.views++;
            await _context.SaveChangesAsync();
            return new BlogDetailDto
            {
                id = blog.id,
                title = blog.title,
                description = blog.description,
                content = blog.content,
                views = blog.views,
                created_at = blog.created_at,
                updated_at = blog.updated_at,
                comments = blog.comments.Select(c => new CommentDto
                {
                    id = c.id,
                    blog_id = c.blog_id,
                    content = c.content,
                    created_at = c.created_at,
                    updated_at = c.updated_at
                }).ToList()
            };
        }

        public async Task AddAsync(BlogRequestDto req)
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
        }

        public async Task UpdateAsync(int id, BlogRequestDto req)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                throw new HttpException("Blog not found", 404);
            }
            blog.title = req.title;
            blog.description = req.description;
            blog.content = req.content;
            blog.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog == null)
            {
                throw new HttpException("Blog not found", 404);
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }
}
