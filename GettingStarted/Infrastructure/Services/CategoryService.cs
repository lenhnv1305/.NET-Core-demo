using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.DTOs;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repo;
        public CategoryService(IGenericRepository<Category> repo)
        {
            this._repo = repo;
        }
        public void Create(CategoryDto entity)
        {
            this._repo.Create(new Category() {
                Id = Guid.NewGuid().ToString(),
                Description = entity.Description,
                Name = entity.Name
            });
        }

        public void Delete(string id)
        {            
            this._repo.Delete(this._repo.GetById(id));
        }

        public CategoryDto GetById(string id)
        {
            var entity = this._repo.GetById(id);
            var returnEntity = new CategoryDto();
            if (entity != null)
            {
                returnEntity.Id = entity.Id;
                returnEntity.Name = entity.Name;
                returnEntity.Description = entity.Description;
            }
            return returnEntity;
        }

        public IEnumerable<CategoryDto> Gets()
        {
            var entities = this._repo.Gets();
            var returnEntities = new List<CategoryDto>();
            if (entities != null)
            {
                foreach(var item in entities)
                {
                    returnEntities.Add(new CategoryDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
            }
            return returnEntities;
        }

        public void Update(CategoryDto entity)
        {
            var category = this._repo.GetById(entity.Id);
            if (category != null)
            {
                category.Name = entity.Name;
                category.Description = entity.Description;
                this._repo.Update(category);
            }
        }
        public List<SelectListItem> CategoriesSelectList(string id = "")
        {
            var categories = this._repo.Gets();
            var categorySelectList = new List<SelectListItem>();
            if (categories != null)
            {
                foreach (var item in categories)
                    categorySelectList.Add(new SelectListItem
                    {
                        Value = item.Id,
                        Text = item.Name,
                        Selected = item.Id.ToLower() == id.ToLower()
                    });
            }
            return categorySelectList;
        }
    }
}
