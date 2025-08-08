using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MyApi.DTOs;
using MyApi.Repositories;
using MyApi.Services;
using MyApi.Models;

namespace MyApi.Tests
{
    public class BlogServiceTests
    {
        [Fact]
        public void TestGetById()
        {
            var repoMock = new Mock<IBlogRepository>();
            var service = new BlogService(repoMock.Object);

            var result = service.GetByIdAsync(6666);

            Assert.NotNull(result);
        }

        [Fact]  
        public async Task UpdateBlog_ShouldReturnUpdatedBlog()
        {
            var request = new BlogRequestDto
            {
                content = "Content Baru",
                description = "Description Baru",
                title = "Title 1 Baru"
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
            blog.title = request.title;
            blog.description = request.description;
            blog.content = request.content;


            var repoMock = new Mock<IBlogRepository>();
            repoMock.Setup(r => r.UpdateAsync(blog)).ReturnsAsync(blog);

            var blogService = new BlogService(repoMock.Object);
            var result = await blogService.UpdateAsync(blog);

            // assert
            Assert.Equal("Content Baru", blog.content);

            repoMock.Verify(r => r.UpdateAsync(blog), Times.Once);
        }

        [Fact]
        public async Task GetBlogById_ShouldReturnCorrectBlog()
        {
            // arrage
            var blog = new Blog
            {
                id = 1,
                title = "Unit Test Title",
                description = "Unit test description",
                content = "Unit test content",
                views = 10,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            var repoMock = new Mock<IBlogRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(blog);

            var blogService = new BlogService(repoMock.Object);

            // act
            var result = await blogService.GetByIdAsync(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal("Unit Test Title", result.title);
            Assert.Equal(10, result.views);

            // verifikasi jika method repo dipanggil
            repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        public async Task CreateBlog_ShouldReturnCreatedBlog()
        {
            var request = new BlogRequestDto
            {
                content = "Content",
                description = "Description",
                title = "Title 1"
            };

            var blog = new Blog
            {
                id = 1,
                content = request.content,
                description = request.description,
                title = request.title,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                views = 0
            };

            var repoMock = new Mock<IBlogRepository>();
            repoMock.Setup(r => r.AddAsync(request)).ReturnsAsync(blog);

            var blogService = new BlogService(repoMock.Object);

            // act
            var result = await blogService.AddAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Title 1", blog.title);
            Assert.Equal(0, blog.views);
            Assert.Equal(1, blog.id);

            // verify
            repoMock.Verify(r => r.AddAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetAllBlogs_ShouldReturnAllBlogs()
        {
            // arrange
            var blogs = new List<Blog>{
                new Blog
                {
                    id = 1,
                    title = "Blog title 1",
                    description = "Blog description 1",
                    views = 10,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                    content = "Blog content 1"
                },
                new Blog
                {
                    id = 2,
                    title = "Blog title 2",
                    description = "Blog description 2",
                    views = 20,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                    content = "Blog content 1"
                }
            };

            var repoMock = new Mock<IBlogRepository>();
            repoMock.Setup(r => r.GetAllAsync(null, null)).ReturnsAsync(blogs);

            var blogService = new BlogService(repoMock.Object);

            // act
            var result = await blogService.GetAllAsync(null, null);

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, b => b.title == "Blog title 1");
            Assert.Contains(result, b => b.title == "Blog title 2");

            // verif
            repoMock.Verify(r => r.GetAllAsync(null, null), Times.Once);
        }
    }
}
