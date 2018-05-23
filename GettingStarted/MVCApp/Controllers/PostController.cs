using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Models.PostViewModels;
using MVCApp.Services;

namespace MVCApp.Controllers
{
    [Route("[controller]/[action]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Modify", new PostViewModel() {
                CategoryId = _postService.Gets().FirstOrDefault()?.Id
            });
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _postService.Create(new Post()
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
            var postViewModel = new PostViewModel();
            var post = _postService.GetById(id);
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
                var post = _postService.GetById(model.Id);
                if (post != null)
                {
                    post.CategoryId = model.CategoryId;
                    post.Title = model.Title;
                    post.ShortDescription = model.ShortDescription;
                    post.Content = model.Content;
                    post.ThumbnailImage = model.ThumbnailImage;
                    post.UpdatedDate = DateTime.UtcNow;
                }
                _postService.Update(post);
            }

            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        public IActionResult Delete(string id = "")
        {
            var post = _postService.GetById(id);
            if (post != null)
            {
                _postService.Delete(post);
            }

            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            return View("Post", _postService.Gets().AsQueryable().Select(c => new PostViewModel
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