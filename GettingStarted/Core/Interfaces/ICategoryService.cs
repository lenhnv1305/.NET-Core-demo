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
        Task<CategoryDto> GetById(string id);
        Task<IEnumerable<CategoryDto>> Gets();
        Task<string> Create(CategoryDto entity);
        Task<string> Update(CategoryDto entity);
        Task<string> Delete(string id);
        Task<List<SelectListItem>> CategoriesSelectList(string id = "");
    }
}
