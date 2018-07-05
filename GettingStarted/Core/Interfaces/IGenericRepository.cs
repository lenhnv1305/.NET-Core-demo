using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(string id);
        Task<IEnumerable<T>> Gets();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
