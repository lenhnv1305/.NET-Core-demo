using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomePageService _homePageService;
        private readonly ICategoryService _categoryService;
        public HomeController(IHomePageService homePageService, ICategoryService categoryService)
        {
            _homePageService = homePageService;
            _categoryService = categoryService;
        }
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
                Posts = _homePageService.FilterPostByCategoryId(categoryId).Select(p => new Models.PostViewModels.PostViewModel
                {
                    CategoryId = p.CategoryId,
                    Id = p.Id,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    ShortDescription = p.ShortDescription,
                    ThumbnailImage = null,
                    Title = p.Title,
                    UpdatedDate = p.UpdatedDate
                }).ToList()
            };
            return View(homepageViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
