using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBlogImageService
    {
        BlogImageDto GetBlogIamge(string id = "", string name = "");
        BlogImageDto Insert(string id, string name, byte[] fileData);
        void Delete(string id);
    }
}
