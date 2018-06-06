using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _repo;
        public PostService(IGenericRepository<Post> repo)
        {
            this._repo = repo;
        }

        public void Create(PostDto entity)
        {
            this._repo.Create(new Post()
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = entity.CategoryId,
                Title = entity.Content,
                ShortDescription = entity.ShortDescription,
                Content = entity.Content,
                ThumbnailImage = entity.ThumbnailImage,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            });
        }

        public void Delete(string id)
        {
            this._repo.Delete(this._repo.GetById(id));
        }

        public PostDto GetById(string id)
        {
            var entity = this._repo.GetById(id);
            var returnEntity = new PostDto();
            if (entity != null)
            {
                returnEntity.Id = entity.Id;
                returnEntity.CategoryId = entity.CategoryId;
                returnEntity.Content = entity.Content;
                returnEntity.ShortDescription = entity.ShortDescription;
                returnEntity.ThumbnailImage = entity.ThumbnailImage;
                returnEntity.Title = entity.Title;
                returnEntity.UpdatedDate = entity.UpdatedDate;
                returnEntity.CreatedDate = entity.CreatedDate;
            }
            return returnEntity;
        }

        public IEnumerable<PostDto> Gets()
        {
            var entities = this._repo.Gets();
            var returnEntities = new List<PostDto>();
            if(entities != null)
            {
                foreach (var item in entities)
                {
                    returnEntities.Add(new PostDto
                    {
                        Id = item.Id,
                        CategoryId = item.CategoryId,
                        Content = item.Content,
                        ShortDescription = item.ShortDescription,
                        ThumbnailImage = item.ThumbnailImage,
                        Title = item.Title,
                        UpdatedDate = item.UpdatedDate,
                        CreatedDate = item.CreatedDate
                    });
                }
            }
            return returnEntities;
        }

        public void Update(PostDto entity)
        {
            var post = this._repo.GetById(entity.Id);
            if (post != null)
            {
                post.CategoryId = entity.CategoryId;
                post.Title = entity.Title;
                post.ShortDescription = entity.ShortDescription;
                post.Content = entity.Content;
                post.ThumbnailImage = entity.ThumbnailImage;
                post.UpdatedDate = DateTime.UtcNow;
            }
            this._repo.Update(post);
        }
    }
}
