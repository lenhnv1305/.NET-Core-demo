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
        public CategoryControllerTest()
        {
        }
        [Fact]
        public void GetCategories_ReturnActionResult()
        {
            //Arrange
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.Gets()).Returns(Task.FromResult(GetCategoryTest()).Result);
            var controller = new CategoryController(mockRepo.Object);

            // Act
            var result = controller.GetCategories();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
            Assert.Equal("01", model.FirstOrDefault().CategoryId);
            Assert.Equal("02", model.LastOrDefault().CategoryId);
        }

        [Fact]
        public void UpdateCategory_ReturnActionResult()
        {
            //Arrange
            var mockRepo = new Mock<ICategoryService>();
            var category = new CategoryDto { Description = "Description", Name = "Name" };
            var categoryViewModel = new CategoryViewModel { Description = "Description", Name = "Name" };
            mockRepo.Setup(repo => repo.Update(category));
            var controller = new CategoryController(mockRepo.Object);

            // Act
            var result = controller.Update(model: categoryViewModel);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetCategories", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName);
        }
        private async Task<IEnumerable<CategoryDto>> GetCategoryTest()
        {
            return new List<CategoryDto>()
            {
                new CategoryDto { Id = "01" },
                new CategoryDto { Id = "02" }
            };
        }
    }
}
