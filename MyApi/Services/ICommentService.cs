using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services
{
    public interface ICommentService
    {
        //Task<IEnumerable<BlogDto>> GetAllAsync(string? searchTitle, string? sort);
        //Task<BlogDetailDto> GetByIdAsync(int id);
        Task<CommentDto> AddAsync(int blogId, CommentRequestDto req);
        Task<CommentDto> UpdateAsync(int id, CommentRequestDto req);
        Task DeleteAsync(int blogId, int id);
    }
}
