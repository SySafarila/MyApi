using Moq;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;
using MyApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Tests
{
    public class CommentServiceTests
    {
        [Fact]
        public async Task CreateComment_ShouldReturnCreatedComment()
        {
            var req = new CommentRequestDto
            {
                content = "Comment",
            };

            var comment = new Comment
            {
                id = 1,
                blog_id = 1,
                content = req.content,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };

            var repoMock = new Mock<ICommentRepository>();
            repoMock.Setup(r => r.AddAsync(comment.blog_id, req)).ReturnsAsync(comment);

            var commentService = new CommentService(repoMock.Object);
            var result = await commentService.AddAsync(comment.blog_id, req);

            Assert.NotNull(result);
            Assert.Equal(req.content, result.content);
            Assert.Equal(comment.id, result.id);

            repoMock.Verify(r => r.AddAsync(comment.blog_id, req), Times.Once);
        }

        [Fact]
        public async Task UpdateComment_ShouldReturnUpdatedComment()
        {
            var req = new CommentRequestDto
            {
                content = "Comment",
            };

            var comment = new Comment
            {
                id = 1,
                blog_id = 1,
                content = req.content,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };

            var repoMock = new Mock<ICommentRepository>();
            repoMock.Setup(r => r.UpdateAsync(comment.id, req)).ReturnsAsync(comment);

            var commentService = new CommentService(repoMock.Object);
            var result = await commentService.UpdateAsync(comment.id, req);

            Assert.NotNull(result);
            Assert.Equal(result.content, req.content);
            Assert.Equal(result.id, comment.id);

            repoMock.Verify(r => r.UpdateAsync(comment.id, req), Times.Once);
        }

        [Fact]
        public async Task DeleteComment_ShouldReturnDeletedComment()
        {
            var comment = new Comment
            {
                id = 1,
                blog_id = 1,
                content = "Content",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };

            var repoMock = new Mock<ICommentRepository>();
            repoMock.Setup(r => r.DeleteAsync(comment.blog_id, comment.id)).ReturnsAsync(comment);

            var commentService = new CommentService(repoMock.Object);
            var result = await commentService.DeleteAsync(comment.blog_id, comment.id);

            Assert.NotNull(result);
            Assert.Equal(result.id, comment.id);

            repoMock.Verify(r => r.DeleteAsync(comment.blog_id, comment.id), Times.Once);
        }
    }
}
