using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Models;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class CategoryTest
    {
        private readonly ICategoryService _categoryService;
        private readonly IGenericRepository<Category> _repo;
        public CategoryTest()
        {
            
        }
        [Fact]
        public void CreateCategory()
        {
            var model = new CategoryDto()
            {
                Description = "Description",
                Name = "Name",
                Id = Guid.NewGuid().ToString()
            };
            var categoryDto = new CategoryDto() { Id = "CategoryId" };
            var category = new Category() { Id = "CategoryId" };
            var categoryServiceMock = new Mock<ICategoryService>();
            var repoMock = new Mock<IGenericRepository<Category>>();
            categoryServiceMock.Setup(x => x.Create(categoryDto));
            repoMock.Setup(x => x.Create(category));

            try
            {
                
                _categoryService.Create(model);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}
