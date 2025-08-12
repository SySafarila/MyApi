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

        public async Task<Comment> AddAsync(int blogId, CommentRequestDto request)
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

            return comment;
        }

        public async Task<Comment> DeleteAsync(int blogId, int id)
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
            return comment;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new HttpException("Comment not found", 404);
            }

            return comment;
        }

        public async Task<Comment> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
