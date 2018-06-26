using Core.DTOs;
using Core.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class HomePageService : IHomePageService
    {
        private readonly IPostService _postService;
        public HomePageService(IPostService postService)
        {
            _postService = postService;
        }
        public IEnumerable<PostDto> FilterPostByCategoryId(string categoryId, string ownerId)
        {
            var query = _postService.Gets();
            if (!string.IsNullOrEmpty(categoryId))
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            return query.ToList();
        }

        public PostDto GetPost(string slug)
        {
            return _postService.Gets(slug: slug).FirstOrDefault();
        }
    }
}
