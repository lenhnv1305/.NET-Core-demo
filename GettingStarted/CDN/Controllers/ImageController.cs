using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CDN.Controllers
{
    public class ImageController : Controller
    {
        private readonly IBlogImageService _blogImageService;

        public ImageController(IBlogImageService blogImageService)
        {
            this._blogImageService = blogImageService;
        }

        public IActionResult Image(string id = "", string name ="")
        {
            var blogImage = _blogImageService.GetBlogIamge(id, name);
            return File(blogImage.BinaryData, "application/jpg");
        }

        public IActionResult Index()
        {
            return Content("Active");
        }
    }
}