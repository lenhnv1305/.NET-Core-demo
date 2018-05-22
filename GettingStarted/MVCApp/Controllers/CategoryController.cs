using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Models.CategoryViewModels;

namespace MVCApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Data.ApplicationDbContext _context;
        public CategoryController(Data.ApplicationDbContext context)
        {
            _context = context;
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
                var repoCategory = new Services.GenericRepository<Category>(_context);
                repoCategory.Create(new Category()
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
                var repoCategory = new Services.GenericRepository<Category>(_context);
                var category = repoCategory.GetById(model.CategoryId);
                if (category != null)
                {
                    category.Name = model.Name;
                    category.Description = model.Description;
                    repoCategory.Update(category);
                }                
            }

            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public IActionResult Delete(string categoryId = "")
        {            
            var repoCategory = new Services.GenericRepository<Category>(_context);
            var category = repoCategory.GetById(categoryId);
            if (category != null)
            {
                repoCategory.Delete(category);
            }

            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var repoCategory = new Services.GenericRepository<Category>(_context);
            return View("Category", repoCategory.Gets().AsQueryable().Select(c => new CategoryViewModel {
                CategoryId = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList());
        }
    }
}