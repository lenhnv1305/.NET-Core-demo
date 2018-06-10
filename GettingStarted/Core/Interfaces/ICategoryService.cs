using Core.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto GetById(string id);
        IEnumerable<CategoryDto> Gets();
        void Create(CategoryDto entity);
        void Update(CategoryDto entity);
        void Delete(string id);
        List<SelectListItem> CategoriesSelectList(string id = "");
    }
}
