using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BlogImageService : IBlogImageService
    {
        private readonly IGenericRepository<BlogImage> _repo;
        public BlogImageService(IGenericRepository<BlogImage> repo)
        {
            _repo = repo;
        }

        public async Task Delete(string id)
        {
            var image = await _repo.GetById(id);
            if (image != null)
            {
                await this._repo.Delete(image);
            }
        }

        public async Task<BlogImageDto> GetBlogIamge(string id = "", string name = "")
        {
            var blogImage = (await _repo.Gets()).Where(c => c.Id == id || c.Name.ToLower().Contains(name.ToLower())).FirstOrDefault();
            var returnBlogImage = new BlogImageDto();
            if (blogImage != null)
            {
                returnBlogImage.Name = blogImage.Name;
                returnBlogImage.Id = blogImage.Id;
                returnBlogImage.BinaryData = blogImage.BinaryData;
                returnBlogImage.UpdatedDate = blogImage.UpdatedDate;
            }
            return returnBlogImage;
        }

        public async Task<BlogImageDto> Insert(string id, string name, byte[] fileData)
        {
            await _repo.Create(new BlogImage
            {
                Id = id,
                Name = name,
                BinaryData = fileData,
                UpdatedDate = DateTime.UtcNow
            });
            return new BlogImageDto
            {
                Id = id,
                Name = name,
                BinaryData = fileData,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
