using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using MVCApp.Models.CategoryViewModels;
using MVCApp.ViewComponents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ComponentTest
    {
        [Fact]
        public void Invoke_ModifyCategoryViewComponent()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var expetedCategoryViewModel = new CategoryViewModel()
            {
                Name = "Name",
                Description = "Description"
            };
            categoryServiceMock.Setup(x => x.GetById("Id")).Returns(Task.FromResult(GetByIdResult()).Result);

            var modifyCategoryComponent = new ModifyCategoryViewComponent(categoryServiceMock.Object);
            var result = modifyCategoryComponent.Invoke("Id");

            var viewDataResult = Assert.IsAssignableFrom<ViewViewComponentResult>(result);
            var categoryViewModelResult = Assert.IsAssignableFrom<CategoryViewModel>(viewDataResult.ViewData.Model);
            Assert.IsType<ViewViewComponentResult>(viewDataResult);
            Assert.Equal(expetedCategoryViewModel.Name, categoryViewModelResult.Name);
            Assert.Equal(expetedCategoryViewModel.Description, categoryViewModelResult.Description);
        }
        private async Task<CategoryDto> GetByIdResult()
        {
            return new CategoryDto
            {
                Id = "Id",
                Name = "Name",
                Description = "Description"
            };
        }
    }
}
