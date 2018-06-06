using System;
using System.Collections.Generic;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T GetById(string id)
        {
            return _context.Find<T>(id);
        }

        public IEnumerable<T> Gets()
        {
            return _context.Set<T>();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        
        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
