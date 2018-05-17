using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Models.PostViewModels;

namespace MVCApp.Controllers
{
    [Route("[controller]/[action]")]
    public class PostController : Controller
    {
        private readonly Data.ApplicationDbContext _context;
        public PostController(Data.ApplicationDbContext  context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Modify", new PostViewModel() {
                CatedoryId = "21bc0e8b-8aba-40a5-ac3d-7827913cb520"
            });
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var repoCategory = new Services.GenericRepository<Post>(_context);
                repoCategory.Create(new Post()
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = model.CategoryId,
                    Title = model.Content,
                    ShortDescription = model.ShortDescription,
                    Content = model.Content,
                    ThumbnailImage = model.ThumbnailImage,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                });
            }

            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var repoCategory = new Services.GenericRepository<Post>(_context);
            return View("Post", repoCategory.Gets().AsQueryable().Select(c => new PostViewModel
            {
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Content = c.Content,
                ThumbnailImage = c.ThumbnailImage
            }).ToList());
        }
    }
}