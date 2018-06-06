using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.ViewComponents
{
    public class ModifyCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public ModifyCategoryViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public IViewComponentResult Invoke(string id = "")
        {
            var category = _categoryService.GetById(id);
            var categoryViewModel = new CategoryViewModel();
            if (category != null)
            {                
                categoryViewModel.Name = category.Name;
                categoryViewModel.Description = category.Description;
            }
            ViewBag.IsEditMode = !string.IsNullOrEmpty(id);
            return View(categoryViewModel);
        }
    }
}
