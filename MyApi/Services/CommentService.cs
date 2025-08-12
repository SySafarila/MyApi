using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> AddAsync(int blogId, CommentRequestDto req)
        {
            var comment = await _commentRepository.AddAsync(blogId, req);
            return comment;
        }

        public async Task<Comment> DeleteAsync(int blogId, int id)
        {
           var comment = await _commentRepository.DeleteAsync(blogId, id);
            return comment;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            return comment;
        }

        public async Task<Comment> UpdateAsync(Comment comment)
        {
            return await _commentRepository.UpdateAsync(comment);
        }
    }
}
