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
            return View("Create", new CategoryViewModel());
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
        public IActionResult GetCategories()
        {
            var repoCategory = new Services.GenericRepository<Category>(_context);
            return View("Category", repoCategory.Gets().AsQueryable().Select(c => new CategoryViewModel {
                Name = c.Name,
                Description = c.Description
            }).ToList());
        }
    }
}