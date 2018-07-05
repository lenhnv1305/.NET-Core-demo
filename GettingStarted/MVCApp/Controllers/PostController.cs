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
using Microsoft.AspNetCore.Identity;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using MVCApp.Authorization;
using MVCApp.Extensions;

namespace MVCApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PostController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IBlogImageService _blogImageService;
        public PostController(
            IPostService postService, 
            ICategoryService categoryService, 
            IBlogImageService blogImageService, 
            UserManager<ApplicationUser> userManager, 
            IAuthorizationService authorizationService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _blogImageService = blogImageService;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var isBloger = HttpContext.User.IsInRole(Constants.BlogerRole);
            if (!isBloger)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Modify", new PostViewModel()
            {
                Categories = await _categoryService.CategoriesSelectList()
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(PostViewModel model, IFormCollection files, string returnUrl = null)
        {
            var isBloger = HttpContext.User.IsInRole(Constants.BlogerRole);
            if (!isBloger)
            {
                return RedirectToAction("Index", "Home");
            }
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
                        UpdatedDate = DateTime.UtcNow,
                        OwnerId = _userManager.GetUserId(HttpContext.User),
                        Slug = StringHelper.UrlFriendly(model.Title)
                    });
                    _blogImageService.Insert(imageId, fileName, bytes);
                }
                catch
                {
                    throw;
                }
            }
            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Update(string id = "")
        {
            var isBloger = HttpContext.User.IsInRole(Constants.BlogerRole);
            if (!isBloger)
            {
                return RedirectToAction("Index", "Home");
            }
            var postViewModel = new PostViewModel();
            try
            {
                var post = await _postService.GetById(id);
                if (post != null)
                {
                    postViewModel.Id = post.Id;
                    postViewModel.CategoryId = post.CategoryId;
                    postViewModel.ShortDescription = post.ShortDescription;
                    postViewModel.Title = post.Title;
                    postViewModel.CategoryId = post.CategoryId;
                    postViewModel.Content = post.Content;
                    postViewModel.ThumbnailImage = post.ThumbnailImage;
                    postViewModel.Categories = await _categoryService.CategoriesSelectList(id);
                    postViewModel.Slug = post.Slug;
                }
            }
            catch
            {
                throw;
            }
            return View("Modify", postViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Update(PostViewModel model, string returnUrl = null)
        {
            var isBloger = HttpContext.User.IsInRole(Constants.BlogerRole);
            if (!isBloger)
            {
                return RedirectToAction("Index", "Home");
            }
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
                var post = await _postService.GetById(model.Id);
                if (post != null)
                {
                    post.CategoryId = model.CategoryId;
                    post.Title = model.Title;
                    post.ShortDescription = model.ShortDescription;
                    post.Content = model.Content;
                    post.ThumbnailImage = fileName + "__" + imageId;
                    post.UpdatedDate = DateTime.UtcNow;
                }
                await _postService.Update(post);
                await _blogImageService.Delete(post.ImageId);
                await _blogImageService.Insert(imageId, fileName, bytes);
            }

            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Delete(string id = "")
        {
            var isBloger = HttpContext.User.IsInRole(Constants.BlogerRole);
            if (!isBloger)
            {
                return RedirectToAction("Index", "Home");
            }
            _postService.Delete(id);
            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts()
        {
            var isBloger = HttpContext.User.IsInRole(Constants.BlogerRole);
            return View("Post", (await _postService.Gets(isBloger, _userManager.GetUserId(HttpContext.User)))
            .Select(c => new PostViewModel
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Content = c.Content,
                ThumbnailImage = c.ThumbnailImage,
                Slug = c.Slug
            }).OrderByDescending(x => x.CreatedDate));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Image(string id = "", string name = "")
        {
            var blogImage = await _blogImageService.GetBlogIamge(id, name);
            return File(blogImage.BinaryData, "image/jpeg");
        }
    }
}