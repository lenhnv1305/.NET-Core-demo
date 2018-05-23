using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public interface ICategoryService
    {
        Category GetById(string id);
        IEnumerable<Category> Gets();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
