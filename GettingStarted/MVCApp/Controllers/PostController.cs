﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.Models.PostViewModels;

namespace MVCApp.Controllers
{
    [Route("[controller]/[action]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public PostController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("Modify", new PostViewModel() {
                Categories = _categoryService.CategoriesSelectList()
            });
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _postService.Create(new PostDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = model.CategoryId,
                    Title = model.Title,
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
                postViewModel.Categories = _categoryService.CategoriesSelectList(id);
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
                ThumbnailImage = c.ThumbnailImage
            }));
        }
    }
}