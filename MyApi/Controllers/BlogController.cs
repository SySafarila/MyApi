using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Requests;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogController : Controller
    {
        [HttpGet("{id}")]
        public ActionResult Detail(int id)
        {
            var blog = new Blog();
            blog.id = id;
            blog.title = "Title for the first article";
            blog.description = "Description for the first article";
            blog.content = "Content for the first article";
            blog.updated_at = DateTime.Now;
            blog.created_at = DateTime.Now;

            return Ok(blog);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> All()
        {
            var blogs = new List<Blog>
            {
                new Blog
                {
                    id = 1,
                    title = "Title 1",
                    description = "Description 1",
                    content = "Content 1",
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                },
                new Blog
                {
                    id = 2,
                    title = "Title 2",
                    description = "Description 2",
                    content = "Content 2",
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                }
            };

            return Ok(blogs);
        }

        [HttpPost]
        public ActionResult Store([FromBody] BlogRequest data)
        {
            var blog = new Blog
            {
                id = 1,
                content = data.content,
                description = data.description,
                title = data.title,
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
            };

            return Ok(blog);
        }
    }
}
