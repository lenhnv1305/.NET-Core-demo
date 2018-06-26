using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IHomePageService
    {
        IEnumerable<PostDto> FilterPostByCategoryId(string categoryId, string ownerId);
        PostDto GetPost(string slug);
    }
}
