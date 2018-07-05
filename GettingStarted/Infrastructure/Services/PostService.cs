using Core.DTOs;
using Core.Interfaces;
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

        public async Task Create(PostDto entity)
        {
            await this._repo.Create(new Post()
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                ShortDescription = entity.ShortDescription,
                Content = entity.Content,
                ThumbnailImage = entity.ThumbnailImage,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                OwnerId = entity.OwnerId,
                Slug = entity.Slug
            });
        }

        public async Task Delete(string id)
        {
            await this._repo.Delete(await this._repo.GetById(id));
        }

        public async Task<PostDto> GetById(string id)
        {
            var entity = await this._repo.GetById(id);
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
                returnEntity.Slug = entity.Slug;
            }
            return returnEntity;
        }

        public async Task<IEnumerable<PostDto>> Gets(bool isBloger = false, string ownerId = "", string postId = "", string slug = "")
        {
            var entities = await this._repo.Gets();
            if (isBloger)
            {
                entities = entities.Where(x => x.OwnerId == ownerId);
            }
            if (!string.IsNullOrEmpty(postId))
            {
                entities = entities.Where(x => x.Id == postId);
            }

            if (!string.IsNullOrEmpty(slug))
            {
                entities = entities.Where(x => x.Slug.ToLower() == slug.ToLower());
            }
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
                        CreatedDate = item.CreatedDate,
                        Slug = item.Slug
                    });
                }
            }
            return returnEntities;
        }

        public async Task Update(PostDto entity)
        {
            var post = await this._repo.GetById(entity.Id);
            if (post != null)
            {
                post.CategoryId = entity.CategoryId;
                post.Title = entity.Title;
                post.ShortDescription = entity.ShortDescription;
                post.Content = entity.Content;
                post.ThumbnailImage = entity.ThumbnailImage;
                post.UpdatedDate = DateTime.UtcNow;
            }
            await this._repo.Update(post);
        }
    }
}
