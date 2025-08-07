using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MyApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CommentDto> AddAsync(int blogId, CommentRequestDto request)
        {
            var comment = new Comment
            {
                blog_id = blogId,
                content = request.content,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return new CommentDto { blog_id = blogId, content = comment.content };
        }

        public async Task DeleteAsync(int blogId, int id)
        {
            var query = _context.Comments.AsQueryable();
            query = query.Where(c => c.blog_id == blogId && c.id == id);
            var comment = await query.FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new HttpException("Comment not found", 404);
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new HttpException("Comment not found", 404);
            }

            return new CommentDto
            {
                blog_id = comment.blog_id,
                content = comment.content,
                created_at = comment.created_at,
                updated_at = comment.updated_at,
                id = comment.id
            };
        }

        public async Task<CommentDto> UpdateAsync(int id, CommentRequestDto request)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new HttpException("Comment not found", 404);
            }

            comment.content = request.content;
            comment.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CommentDto
            {
                id = comment.id,
                blog_id = comment.blog_id,
                content = comment.content,
                created_at = comment.created_at,
                updated_at = comment.updated_at
            };
        }
    }
}
