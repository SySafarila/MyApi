using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories
{
    public interface ICommentRepository
    {
        //Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort);
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> AddAsync(int blogId, CommentRequestDto request);
        Task<Comment> UpdateAsync(Comment comment);
        Task<Comment> DeleteAsync(int blogId, int id);
    }
}
