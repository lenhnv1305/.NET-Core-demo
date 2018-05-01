using System;
using System.Collections.Generic;
using System.Text;

namespace MVCApp.Services
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);
        IEnumerable<T> Gets();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
