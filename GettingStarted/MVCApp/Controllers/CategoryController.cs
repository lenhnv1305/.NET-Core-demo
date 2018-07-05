using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models.CategoryViewModels;

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
                _categoryService.Create(new Core.DTOs.CategoryDto
                {
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
                _categoryService.Update(new Core.DTOs.CategoryDto
                {
                    Name = model.Name,
                    Description = model.Description
                });           
            }
            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string categoryId = "")
        {            
            await _categoryService.Delete(categoryId);
            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return View("Category", (await _categoryService.Gets()).Select(c => new CategoryViewModel {
                CategoryId = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList());
        }
    }
}