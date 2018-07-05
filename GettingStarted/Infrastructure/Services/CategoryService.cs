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

        public async Task<string> Create(CategoryDto entity)
        {
            var newGuidId = Guid.NewGuid().ToString();
            await this._repo.Create(new Category() {
                Id = newGuidId,
                Description = entity.Description,
                Name = entity.Name
            });
            return newGuidId;
        }

        public async Task<string> Delete(string id)
        {
            var category = await this._repo.GetById(id);
            if (category != null)
            {
                await this._repo.Delete(category);
                return category.Id;
            }
            return string.Empty;
        }

        public async Task<CategoryDto> GetById(string id)
        {
            var entity = await this._repo.GetById(id);
            var returnEntity = new CategoryDto();
            if (entity != null)
            {
                returnEntity.Id = entity.Id;
                returnEntity.Name = entity.Name;
                returnEntity.Description = entity.Description;
            }
            return returnEntity;
        }

        public async Task<IEnumerable<CategoryDto>> Gets()
        {
            var entities = await this._repo.Gets();
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

        public async Task<string> Update(CategoryDto entity)
        {
            var category = await this._repo.GetById(entity.Id);
            if (category != null)
            {
                category.Name = entity.Name;
                category.Description = entity.Description;
                this._repo.Update(category);
                return category.Id;
            }
            return string.Empty;
        }
        public async Task<List<SelectListItem>> CategoriesSelectList(string id = "")
        {
            var categories = await this._repo.Gets();
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
