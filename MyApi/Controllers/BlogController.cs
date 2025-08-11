using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public BlogController(IBlogService blogService, ICommentService commentService, IMapper mapper)
        {
            _blogService = blogService;
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAll([FromQuery] string? searchTitle, [FromQuery] string? sort = "asc")
        {
            var blogs = await _blogService.GetAllAsync(searchTitle?.ToLower(), sort?.ToLower());
            var blogDtos = _mapper.Map<IEnumerable<BlogDto>>(blogs);

            return Ok(blogDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailDto>> GetById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            blog.views++;
            await _blogService.UpdateAsync(blog);
            var blogDetailDto = _mapper.Map<BlogDetailDto>(blog);

            return Ok(blogDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<BlogDetailDto>> Store(BlogRequestDto req)
        {
            var blog = await _blogService.AddAsync(req);
            var blogDetailDto = _mapper.Map<BlogDetailDto>(blog);

            return Ok(blogDetailDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogDetailDto>> Delete(int id)
        {
            var blog = await _blogService.DeleteAsync(id);
            var blogDetailDto = _mapper.Map<BlogDetailDto>(blog);
            return Ok(blogDetailDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BlogDetailDto>> Update(int id, BlogRequestDto req)
        {
            var blog = await _blogService.GetByIdAsync(id);
            blog.title = req.title;
            blog.description = req.description;
            blog.content = req.content;
            var blogUpdate = await _blogService.UpdateAsync(blog);
            var blogDetailDto = _mapper.Map<BlogDetailDto>(blog);

            return Ok(blogDetailDto);
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<CommentDto>> SendComment(int id, CommentRequestDto req)
        {
            var blog = await _blogService.GetByIdAsync(id);
            var comment = await _commentService.AddAsync(blog.id, req);
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        [HttpDelete("{blogId}/comments/{commentId}")]
        public async Task<ActionResult<CommentDto>> DeleteComment(int blogId, int commentId)
        {
            var comment = await _commentService.DeleteAsync(blogId, commentId);
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }
    }
}
