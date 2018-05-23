using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public interface IPostService
    {
        Post GetById(string id);
        IEnumerable<Post> Gets();
        void Create(Post entity);
        void Update(Post entity);
        void Delete(Post entity);
    }
}
