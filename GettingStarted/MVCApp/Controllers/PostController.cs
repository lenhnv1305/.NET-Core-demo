using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.Models.PostViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCApp.Controllers
{
    [Route("[controller]/[action]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IBlogImageService _blogImageService;
        public PostController(IPostService postService, ICategoryService categoryService, IBlogImageService blogImageService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _blogImageService = blogImageService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Modify", new PostViewModel() {
                Categories = _categoryService.CategoriesSelectList()
            });
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model, IFormCollection files, string returnUrl = null)
        {

            byte[] bytes = new byte[] { };
            var fileName = "";
            if (Request.Form.Files[0] != null)
            {
                var length = Request.Form.Files[0].Length;
                using (BinaryReader reader = new BinaryReader(Request.Form.Files[0].OpenReadStream()))
                {
                    bytes = reader.ReadBytes((int)length);
                }
                fileName = Request.Form.Files[0].FileName;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var imageId = Guid.NewGuid().ToString();
                    _postService.Create(new PostDto()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CategoryId = model.CategoryId,
                        Title = model.Title,
                        ShortDescription = model.ShortDescription,
                        Content = model.Content,
                        ThumbnailImage = fileName + "__" + imageId,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    });
                    _blogImageService.Insert(imageId , fileName, bytes);
                }
                catch
                {
                    throw;
                }                
            }
            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        public IActionResult Update(string id = "")
        {
            var postViewModel = new PostViewModel();
            try
            {
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
                    postViewModel.Categories = _categoryService.CategoriesSelectList(id);
                }
            }
            catch
            {
                throw;
            }            
            return View("Modify", postViewModel);
        }

        [HttpPost]
        public IActionResult Update(PostViewModel model, string returnUrl = null)
        {
            byte[] bytes = new byte[] { };
            var fileName = "";
            if (Request.Form.Files[0] != null)
            {
                var length = Request.Form.Files[0].Length;
                using (BinaryReader reader = new BinaryReader(Request.Form.Files[0].OpenReadStream()))
                {
                    bytes = reader.ReadBytes((int)length);
                }
                fileName = Request.Form.Files[0].FileName;
            }
            if (ModelState.IsValid)
            {
                var imageId = Guid.NewGuid().ToString();
                var post = _postService.GetById(model.Id);
                if (post != null)
                {
                    post.CategoryId = model.CategoryId;
                    post.Title = model.Title;
                    post.ShortDescription = model.ShortDescription;
                    post.Content = model.Content;
                    post.ThumbnailImage = fileName + "__" + imageId;
                    post.UpdatedDate = DateTime.UtcNow;
                }
                _postService.Update(post);
                _blogImageService.Delete(post.ImageId);
                _blogImageService.Insert(imageId, fileName, bytes);
            }

            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        public IActionResult Delete(string id = "")
        {
            _postService.Delete(id);
            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            return View("Post", _postService.Gets().Select(c => new PostViewModel
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Content = c.Content,
                ThumbnailImage = null
            }));
        }
}