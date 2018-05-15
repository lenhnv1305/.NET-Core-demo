using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using MVCApp.Models;
using MVCApp.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.ViewComponents
{
    public class ModifyCategoryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ModifyCategoryViewComponent(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IViewComponentResult Invoke(string id = "")
        {
            var category = new Services.GenericRepository<Category>(_context).GetById(id);
            var categoryViewModel = new CategoryViewModel();
            if (category != null)
            {                
                categoryViewModel.Name = category.Name;
                categoryViewModel.Description = category.Description;
            }
            ViewBag.IsEditMode = string.IsNullOrEmpty(id);
            return View(categoryViewModel);
        }
    }
}
