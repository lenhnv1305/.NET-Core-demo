using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCApp.Models;

namespace MVCApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repo;
        public CategoryService(IGenericRepository<Category> repo)
        {
            this._repo = repo;
        }
        public void Create(Category entity)
        {
            this._repo.Create(entity);
        }

        public void Delete(Category entity)
        {
            this._repo.Delete(entity);
        }

        public Category GetById(string id)
        {
            return this._repo.GetById(id);
        }

        public IEnumerable<Category> Gets()
        {
            return this._repo.Gets();
        }

        public void Update(Category entity)
        {
            this._repo.Update(entity);
        }
    }
}
