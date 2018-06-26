using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPostService
    {
        PostDto GetById(string id);
        IEnumerable<PostDto> Gets(bool isBloger = false, string ownerId = "", string postId = "", string slug = "");
        void Create(PostDto entity);
        void Update(PostDto entity);
        void Delete(string id);
    }
}
