using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApi.Controllers;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Tests
{
    public class BlogControllerTests
    {
        [Fact]
        public async Task GetDetailBlogById()
        {
            var blog = new Blog
            {
                id = 1,
                content = "Content",
                title = "title",
                description = "description",
                views = 10,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };

            var blogDetail = new Blog
            {
                id = blog.id,
                content = blog.content,
                title = blog.title,
                description = blog.description,
                views = blog.views + 1,
                created_at = blog.created_at,
                updated_at = blog.updated_at,
            };

            var mockService = new Mock<IBlogService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.GetByIdAsync(blog.id)).ReturnsAsync(blog);
            mockService.Setup(s => s.UpdateAsync(It.IsAny<Blog>())).ReturnsAsync(blogDetail);

            mockMapper.Setup(m => m.Map<BlogDetailDto>(blogDetail)).Returns(new BlogDetailDto
            {
                id = 1,
                content = "Content",
                title = "title",
                description = "description",
                views = 11,
                created_at = blog.created_at,
                updated_at = blog.updated_at
            });

            var controller = new BlogController(mockService.Object, null, mockMapper.Object);

            var actionResult = await controller.GetById(1);
            var okResult = actionResult.Result as OkObjectResult;
            var dto = okResult?.Value as BlogDetailDto;

            Assert.NotNull(dto);
            Assert.Equal(11, dto.views);

            mockService.Verify(s => s.GetByIdAsync(blog.id), Times.Once);
            mockService.Verify(s => s.UpdateAsync(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public async Task GetAllBlogs()
        {
            var blogs = new List<Blog>{
                new Blog
                {
                    id = 1,
                    content = "Content",
                    title = "title",
                    description = "description",
                    views = 10,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                },
                new Blog
                {
                    id = 2,
                    content = "Content",
                    title = "title",
                    description = "description",
                    views = 10,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                }
            };

            var blogDtos = new List<BlogDto>
            {
                new BlogDto
                {
                    id = 1,
                    title = "title",
                    description = "description",
                    views = 10,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                },
                new BlogDto
                {
                    id = 2,
                    title = "title",
                    description = "description",
                    views = 10,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                }
            };

            var mockService = new Mock<IBlogService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.GetAllAsync(null, null)).ReturnsAsync(blogs);
            mockMapper.Setup(m => m.Map<IEnumerable<BlogDto>>(blogs)).Returns(blogDtos);

            var controller = new BlogController(mockService.Object, null, mockMapper.Object);

            var actionResult = await controller.GetAll(null, null);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDtos = okResult?.Value as IEnumerable<BlogDto>;

            Assert.NotNull(resultDtos);
            Assert.Equal(2, resultDtos.Count());
            Assert.Contains(resultDtos, b => b.id == 1 && b.title == "title");
            Assert.Contains(resultDtos, b => b.id == 2 && b.title == "title");

            mockService.Verify(s => s.GetAllAsync(null, null), Times.Once);
            mockMapper.Verify(m => m.Map<IEnumerable<BlogDto>>(blogs), Times.Once);
        }

        [Fact]
        public async Task StoreBlog()
        {
            var blogReq = new BlogRequestDto
            {
                content = "Content",
                description = "Description",
                title = "Title"
            };

            var blog = new Blog
            {
                id = 1,
                content = blogReq.content,
                description = blogReq.description,
                title = blogReq.title,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var blogDetail = new BlogDetailDto
            {
                id = 1,
                content = blogReq.content,
                description = blogReq.description,
                title = blogReq.title,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var mockService = new Mock<IBlogService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.AddAsync(blogReq)).ReturnsAsync(blog);
            mockMapper.Setup(m => m.Map<BlogDetailDto>(blog)).Returns(blogDetail);

            var controller = new BlogController(mockService.Object, null, mockMapper.Object);

            var actionResult = await controller.Store(blogReq);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDto = okResult?.Value as BlogDetailDto;

            Assert.NotNull(resultDto);
            Assert.Equal(1, resultDto.id);
            Assert.Equal("Description", resultDto.description);
            Assert.Equal("Content", resultDto.content);

            mockService.Verify(s => s.AddAsync(blogReq), Times.Once());
            mockMapper.Verify(m => m.Map<BlogDetailDto>(blog), Times.Once());
        }

        [Fact]
        public async Task DeleteBlog()
        {
            var blog = new Blog
            {
                id = 1,
                content = "Content",
                description = "Description",
                title = "Title",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var blogDetail = new BlogDetailDto
            {
                id = 1,
                content = "Content",
                description = "Description",
                title = "Title",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var mockService = new Mock<IBlogService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.DeleteAsync(blog.id)).ReturnsAsync(blog);
            mockMapper.Setup(m => m.Map<BlogDetailDto>(blog)).Returns(blogDetail);

            var controller = new BlogController(mockService.Object, null, mockMapper.Object);

            var actionResult = await controller.Delete(blog.id);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDto = okResult?.Value as BlogDetailDto;

            Assert.NotNull(resultDto);
            Assert.Equal(1, resultDto.id);

            mockService.Verify(s => s.DeleteAsync(blog.id), Times.Once);
            mockMapper.Verify(m => m.Map<BlogDetailDto>(blog), Times.Once);
        }

        [Fact]
        public async Task UpdateBlog()
        {
            var blogReq = new BlogRequestDto
            {
                content = "Content 2",
                description = "Description 2",
                title = "Title 2"
            };

            var blog = new Blog
            {
                id = 1,
                content = "Content",
                description = "Description",
                title = "Title",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var updatedBlog = new Blog
            {
                id = 1,
                content = blogReq.content,
                description = blogReq.description,
                title = blogReq.title,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var blogDetail = new BlogDetailDto
            {
                id = 1,
                content = blogReq.content,
                description = blogReq.description,
                title = blogReq.title,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var mockService = new Mock<IBlogService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(s => s.GetByIdAsync(blog.id)).ReturnsAsync(blog);
            mockService.Setup(s => s.UpdateAsync(It.IsAny<Blog>())).ReturnsAsync(updatedBlog);
            mockMapper.Setup(m => m.Map<BlogDetailDto>(It.IsAny<Blog>())).Returns(blogDetail);

            var controller = new BlogController(mockService.Object, null, mockMapper.Object);
            var actionResult = await controller.Update(blog.id, blogReq);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDto = okResult?.Value as BlogDetailDto;

            Assert.NotNull(resultDto);
            Assert.Equal(blogReq.content, resultDto.content);

            mockService.Verify(s => s.GetByIdAsync(blog.id), Times.Once);
            mockService.Verify(s => s.UpdateAsync(It.IsAny<Blog>()), Times.Once);
            mockMapper.Verify(m => m.Map<BlogDetailDto>(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public async Task CreateComment()
        {
            var blog = new Blog
            {
                id = 1,
                content = "Content",
                description = "Description",
                title = "Title",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };
            var commentRequest = new CommentRequestDto
            {
                content = "Comment"
            };
            var comment = new Comment
            {
                id = 1,
                blog_id = blog.id,
                content = commentRequest.content,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };
            var commentDto = new CommentDto
            {
                id = comment.id,
                blog_id = comment.blog_id,
                content = comment.content,
                created_at = comment.created_at,
                updated_at = comment.updated_at
            };


            var mockBlogService = new Mock<IBlogService>();
            var mockCommentService = new Mock<ICommentService>();
            var mockMapper = new Mock<IMapper>();

            mockBlogService.Setup(s => s.GetByIdAsync(blog.id)).ReturnsAsync(blog);
            mockCommentService.Setup(s => s.AddAsync(blog.id, commentRequest)).ReturnsAsync(comment);
            mockMapper.Setup(m => m.Map<CommentDto>(comment)).Returns(commentDto);

            var controller = new BlogController(mockBlogService.Object, mockCommentService.Object, mockMapper.Object);
            var actionResult = await controller.SendComment(blog.id, commentRequest);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDto = okResult?.Value as CommentDto;

            Assert.NotNull(resultDto);
            Assert.Equal(resultDto.content, commentDto.content);
            Assert.Equal(resultDto.id, commentDto.id);

            mockBlogService.Verify(s => s.GetByIdAsync(blog.id), Times.Once);
            mockCommentService.Verify(s => s.AddAsync(blog.id, commentRequest), Times.Once);
            mockMapper.Verify(m => m.Map<CommentDto>(comment), Times.Once);
        }

        [Fact]
        public async Task DeleteComment()
        {
            var blog = new Blog
            {
                id = 1,
                content = "Content",
                description = "Description",
                title = "Title",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };
            var commentRequest = new CommentRequestDto
            {
                content = "Comment"
            };
            var comment = new Comment
            {
                id = 1,
                blog_id = blog.id,
                content = commentRequest.content,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };
            var commentDto = new CommentDto
            {
                id = comment.id,
                blog_id = comment.blog_id,
                content = comment.content,
                created_at = comment.created_at,
                updated_at = comment.updated_at
            };

            var mockCommentService = new Mock<ICommentService>();
            var mockMapper = new Mock<IMapper>();

            mockCommentService.Setup(s => s.DeleteAsync(blog.id, comment.id)).ReturnsAsync(comment);
            mockMapper.Setup(m => m.Map<CommentDto>(comment)).Returns(commentDto);

            var controller = new BlogController(null, mockCommentService.Object, mockMapper.Object);
            var actionResult = await controller.DeleteComment(blog.id, comment.id);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDto = okResult?.Value as CommentDto;

            Assert.NotNull(resultDto);
            Assert.Equal(comment.id, resultDto.id);
            Assert.Equal(comment.content, resultDto.content);

            mockCommentService.Verify(s => s.DeleteAsync(blog.id, comment.id), Times.Once);
            mockMapper.Verify(m => m.Map<CommentDto>(comment), Times.Once);
        }

        [Fact]
        public async Task UpdateComment()
        {
            var comment = new Comment
            {
                id = 1,
                blog_id = 1,
                content = "Content",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
            };
            var commentRequest = new CommentRequestDto
            {
                content = "Content Updated"
            };
            var updatedComment = new Comment
            {
                id = 1,
                blog_id = 1,
                content = "Content Updated",
                created_at = comment.created_at,
                updated_at = DateTime.UtcNow,
            };
            var commentDto = new CommentDto
            {
                id = 1,
                blog_id = 1,
                content = updatedComment.content,
                created_at = updatedComment.created_at,
                updated_at = updatedComment.updated_at
            };

            var mockCommentService = new Mock<ICommentService>();
            var mockMapper = new Mock<IMapper>();

            mockCommentService.Setup(s => s.GetByIdAsync(comment.id)).ReturnsAsync(comment);
            mockCommentService.Setup(s => s.UpdateAsync(It.IsAny<Comment>())).ReturnsAsync(updatedComment);
            mockMapper.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(commentDto);

            var controller = new BlogController(null, mockCommentService.Object, mockMapper.Object);
            var actionResult = await controller.UpdateComment(comment.blog_id, comment.id, commentRequest);
            var okResult = actionResult.Result as OkObjectResult;
            var resultDto = okResult?.Value as CommentDto;

            Assert.NotNull(resultDto);
        }
    }
}
