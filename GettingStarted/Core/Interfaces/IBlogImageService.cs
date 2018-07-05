using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBlogImageService
    {
        Task<BlogImageDto> GetBlogIamge(string id = "", string name = "");
        Task<BlogImageDto> Insert(string id, string name, byte[] fileData);
        Task Delete(string id);
    }
}
