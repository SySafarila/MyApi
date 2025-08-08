using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        public BlogController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAll([FromQuery] string? searchTitle, [FromQuery] string? sort = "asc")
        {
            var blogs = await _blogService.GetAllAsync(searchTitle?.ToLower(), sort?.ToLower());
            var blogDto = blogs.Select(b => new BlogDto
            {
                id = b.id,
                title = b.title,
                description = b.description,
                views = b.views,
                created_at = b.created_at,
                updated_at = b.updated_at
            }).ToList();

            return blogDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailDto>> GetById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            blog.views++;
            await _blogService.UpdateAsync(blog);
            var response = new BlogDetailDto
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
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<BlogDetailDto>> Store(BlogRequestDto req)
        {
            var blog = await _blogService.AddAsync(req);

            return new BlogDetailDto
            {
                id = blog.id,
                title = blog.title,
                description = blog.description,
                content = blog.content,
                views = blog.views,
                created_at = blog.created_at,
                updated_at = blog.updated_at
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return Ok(new { message = "Blog deleted" });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BlogDetailDto>> Update(int id, BlogRequestDto req)
        {
            var blog = await _blogService.GetByIdAsync(id);
            blog.title = req.title;
            blog.description = req.description;
            blog.content = req.content;
            var blogUpdate = await _blogService.UpdateAsync(blog);

            return new BlogDetailDto
            {
                id = blogUpdate.id,
                title = blogUpdate.title,
                description = blogUpdate.description,
                content = blogUpdate.content,
                views = blogUpdate.views,
                created_at = blogUpdate.created_at,
                updated_at = blogUpdate.updated_at
            };
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<CommentDto>> SendComment(int id, CommentRequestDto req)
        {
            var blog = await _blogService.GetByIdAsync(id);
            var comment = await _commentService.AddAsync(blog.id, req);
            return comment;
        }

        [HttpDelete("{blogId}/comments/{commentId}")]
        public async Task<ActionResult> DeleteComment(int blogId, int commentId)
        {
            await _commentService.DeleteAsync(blogId, commentId);

            return Ok(new
            {
                message = "Comment deleted"
            });
        }
    }
}
