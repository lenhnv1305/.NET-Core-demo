using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Models;
using MVCApp.Models.PostViewModels;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAuthorizationService _authorizationService;
        private readonly IHomePageService _homePageService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IHomePageService homePageService, ICategoryService categoryService, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _homePageService = homePageService;
            _categoryService = categoryService;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Index(string categoryId = "")
        {
            var homepageViewModel = new HomePageViewModel()
            {
                Categories = _categoryService.Gets().Select(c => new  Models.CategoryViewModels.CategoryViewModel
                {
                    CategoryId = c.Id,
                    Description = c.Description,
                    Name = c.Name
                }).ToList(),
                Posts = _homePageService.FilterPostByCategoryId(categoryId, _userManager.GetUserId(HttpContext.User))
                .Select(p => new PostViewModel
                {
                    CategoryId = p.CategoryId,
                    Id = p.Id,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    ShortDescription = p.ShortDescription,
                    ThumbnailImage = p.ThumbnailImage,
                    Title = p.Title,
                    UpdatedDate = p.UpdatedDate,
                    Slug = p.Slug
                }).ToList()
            };
            return View(homepageViewModel);
        }

        [AllowAnonymous]
        public IActionResult PostDetail(string slug = "")
        {
            if (Request.Path.HasValue && Request.Path.Value.Split("/").Length > 2)
            {
                slug = Request.Path.Value.Split("/")[2];
            }
            var postDto = _homePageService.GetPost(slug);
            var postViewModel = new PostViewModel();
            if (postDto != null)
            {
                postViewModel.CategoryId = postDto.CategoryId;
                postViewModel.Content = postDto.Content;
                postViewModel.Id = postDto.Id;
                postViewModel.OwnerId = postDto.OwnerId;
                postViewModel.ShortDescription = postDto.ShortDescription;
                postViewModel.ThumbnailImage = postDto.ThumbnailImage;
                postViewModel.Title = postDto.Title;
                postViewModel.UpdatedDate = postDto.UpdatedDate;
                postViewModel.CreatedDate = postDto.CreatedDate;
            }
            return View("PostDetail", postViewModel);
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
