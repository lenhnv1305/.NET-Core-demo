using Core.DTOs;
using Core.Interfaces;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class CategoryTest
    {
        private readonly ICategoryService _categoryService;
        public CategoryTest()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            _categoryService = categoryServiceMock.Object;
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
