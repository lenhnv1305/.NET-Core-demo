using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> GetById(string id);
        Task<IEnumerable<PostDto>> Gets(bool isBloger = false, string ownerId = "", string postId = "", string slug = "");
        Task Create(PostDto entity);
        Task Update(PostDto entity);
        Task Delete(string id);
    }
}
