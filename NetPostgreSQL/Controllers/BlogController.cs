using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetPostgreSQL.Models;
using NetPostgreSQL.Services;

namespace NetPostgreSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {

        private readonly ILogger<BlogController> _logger;
        private readonly IBloggingRepository _repository;

        public BlogController(IBloggingRepository repository, ILogger<BlogController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> GetBlogs()
        {
            return Ok(_repository.GetBlogs().ToArray());
        }

        [HttpGet("{blogId}", Name ="GetBlog")]
        public ActionResult<Blog> GetBlog(int blogId)
        {
            var blog = _repository.GetBlog(blogId);

            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        [HttpPut("{blogId}")]
        public ActionResult<Blog> UpdateBlog(int blogId, Blog blog)
        {
            blog.BlogId = blogId;

            _repository.UpdateBlog(blog);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{blogId}")]
        public ActionResult<Blog> DeleteBlog(int blogId)
        {
            var blog = _repository.GetBlog(blogId);

            if (blog == null)
                return NotFound();

            _repository.DeleteBlog(blog);
            _repository.Save();

            return NoContent();
        }


        [HttpPost]
        public ActionResult<Blog> Post(Blog blog)
        {
            _repository.AddBlog(blog);
            _repository.Save();

            return CreatedAtRoute("GetBlog", new { blogId = blog.BlogId }, blog);
        }
    }
}
