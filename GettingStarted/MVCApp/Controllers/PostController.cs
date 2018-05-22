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
            var repoCategory = new Services.GenericRepository<Category>(_context);
            return View("Modify", new PostViewModel() {
                CategoryId = repoCategory.Gets().FirstOrDefault()?.Id
            });
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var repoPost = new Services.GenericRepository<Post>(_context);
                repoPost.Create(new Post()
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
        public IActionResult Update(string id = "")
        {
            var repoCategory = new Services.GenericRepository<Post>(_context);
            var postViewModel = new PostViewModel();
            var post = repoCategory.GetById(id);
            if (post != null)
            {
                postViewModel.Id = post.Id;
                postViewModel.CategoryId = post.CategoryId;
                postViewModel.ShortDescription = post.ShortDescription;
                postViewModel.Title = post.Title;
                postViewModel.CategoryId = post.CategoryId;
                postViewModel.Content = post.Content;
                postViewModel.ThumbnailImage = post.ThumbnailImage;
            }
            return View("Modify", postViewModel);
        }

        [HttpPost]
        public IActionResult Update(PostViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var repoCategory = new Services.GenericRepository<Post>(_context);
                var post = repoCategory.GetById(model.Id);
                if (post != null)
                {
                    post.CategoryId = model.CategoryId;
                    post.Title = model.Title;
                    post.ShortDescription = model.ShortDescription;
                    post.Content = model.Content;
                    post.ThumbnailImage = model.ThumbnailImage;
                    post.UpdatedDate = DateTime.UtcNow;
                }
                repoCategory.Update(post);
            }

            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        public IActionResult Delete(string id = "")
        {
            var repoPost = new Services.GenericRepository<Post>(_context);
            var post = repoPost.GetById(id);
            if (post != null)
            {
                repoPost.Delete(post);
            }

            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var repoPost = new Services.GenericRepository<Post>(_context);
            return View("Post", repoPost.Gets().AsQueryable().Select(c => new PostViewModel
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Content = c.Content,
                ThumbnailImage = c.ThumbnailImage
            }).ToList());
        }
    }
}