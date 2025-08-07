using MyApi.DTOs;
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

        public async Task<CommentDto> AddAsync(int blogId, CommentRequestDto req)
        {
            var comment = await _commentRepository.AddAsync(blogId, req);
            return comment;
        }

        public async Task DeleteAsync(int blogId, int id)
        {
            await _commentRepository.DeleteAsync(blogId, id);
        }

        public async Task<CommentDto> UpdateAsync(int id, CommentRequestDto req)
        {
            var comment = await _commentRepository.UpdateAsync(id, req);
            return comment;
        }
    }
}
