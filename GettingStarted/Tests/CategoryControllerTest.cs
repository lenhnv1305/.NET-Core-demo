using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCApp.Controllers;
using MVCApp.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CategoryControllerTest
    {
        private readonly ICategoryService _categoryService;
        public CategoryControllerTest()
        {
        }
        [Fact]
        public void GetCategories_ReturnActionResult()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.Gets()).Returns(GetCategoryTest());
            var controller = new CategoryController(mockRepo.Object);

            // Act
            var result = controller.GetCategories();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        private List<CategoryDto> GetCategoryTest()
        {
            return new List<CategoryDto>() { new CategoryDto { Id = "01" }, new CategoryDto { Id = "02" } };
        }
    }
}
