using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _repo;
        public PostService(IGenericRepository<Post> repo)
        {
            this._repo = repo;
        }

        public void Create(Post entity)
        {
            this._repo.Create(entity);
        }

        public void Delete(Post entity)
        {
            this._repo.Delete(entity);
        }

        public Post GetById(string id)
        {
            return this._repo.GetById(id);
        }

        public IEnumerable<Post> Gets()
        {
            return this._repo.Gets();
        }

        public void Update(Post entity)
        {
            this._repo.Update(entity);
        }
    }
}
