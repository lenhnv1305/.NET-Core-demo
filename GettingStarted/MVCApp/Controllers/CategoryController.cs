using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Models.CategoryViewModels;
using MVCApp.Services;

namespace MVCApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Modify");
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(new Category()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Description = model.Description
                });
            }

            return RedirectToAction("GetCategories");
        }
        [HttpGet]
        public IActionResult Update(string categoryId = "")
        {
            return View("Modify", categoryId);
        }

        [HttpPost]
        public IActionResult Update(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryService.GetById(model.CategoryId);
                if (category != null)
                {
                    category.Name = model.Name;
                    category.Description = model.Description;
                    _categoryService.Update(category);
                }                
            }

            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public IActionResult Delete(string categoryId = "")
        {            
            var category = _categoryService.GetById(categoryId);
            if (category != null)
            {
                _categoryService.Delete(category);
            }

            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return View("Category", _categoryService.Gets().AsQueryable().Select(c => new CategoryViewModel {
                CategoryId = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList());
        }
    }
}