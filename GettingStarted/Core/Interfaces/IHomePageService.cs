using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IHomePageService
    {
        Task<IEnumerable<PostDto>> FilterPostByCategoryId(string categoryId, string ownerId);
        Task<PostDto> GetPost(string slug);
    }
}
