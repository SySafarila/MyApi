using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories
{
    public interface ICommentRepository
    {
        //Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort);
        Task<CommentDto> GetByIdAsync(int id);
        Task<CommentDto> AddAsync(int blogId, CommentRequestDto request);
        Task<CommentDto> UpdateAsync(int id, CommentRequestDto request);
        Task DeleteAsync(int blogId, int id);
    }
}
