using Core.DTOs;
using Core.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class HomePageService : IHomePageService
    {
        private readonly IPostService _postService;
        public HomePageService(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IEnumerable<PostDto>> FilterPostByCategoryId(string categoryId, string ownerId)
        {
            var query = await _postService.Gets();
            if (!string.IsNullOrEmpty(categoryId))
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            return query.ToList();
        }

        public async Task<PostDto> GetPost(string slug)
        {
            return (await _postService.Gets(slug: slug)).FirstOrDefault();
        }
    }
}
