using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionPerRequest.Api.Models;
using TransactionPerRequest.Data;
using TransactionPerRequest.Data.Entities;

namespace TransactionPerRequest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BlogController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogModel>>> Get()
        {
           List<Blog> blogs = await dbContext.Set<Blog>()
                .AsQueryable()
                .Include(x => x.Posts)
                .ToListAsync();

            List<BlogModel> blogModels = blogs.Select(b => new BlogModel()
            {
                Url = b.Url,
                Posts = b.Posts?.Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    BlogId = p.BlogId
                }).ToList()
            }).ToList();

            return Ok(blogModels);
        }

        [HttpPost("CreateBlogsWithError")]
        public async Task<ActionResult> CreateBlogsWithError()
        {
            dbContext.Set<Blog>().Add(new Blog()
            {
                Url = "https://theblog.com",
                Posts = new List<Post>()
                {
                    new Post()
                    {
                        Title = "There is error",
                        CreatedDate = DateTime.UtcNow,
                        Content = "The error content"
                    }
                }
            });

            await dbContext.SaveChangesAsync();

            throw new Exception("There is a Error. Transaction rollback.");

            return Ok();
        }

        [HttpPost("CreateBlogsWithoutError")]
        public async Task<ActionResult> CreateBlogsWithoutError()
        {
            dbContext.Set<Blog>().Add(new Blog()
            {
                Url = "https://theblog.com",
                Posts = new List<Post>()
                {
                    new Post()
                    {
                        Title = "No errors",
                        CreatedDate = DateTime.UtcNow,
                        Content = "The content"
                    }
                }
            });

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
