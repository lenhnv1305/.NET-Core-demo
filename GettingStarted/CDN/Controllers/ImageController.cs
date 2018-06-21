using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CDN.Controllers
{
    public class ImageController : Controller
    {
        private readonly IBlogImageService _blogImageService;
        public ImageController(IBlogImageService blogImageService)
        {
            this._blogImageService = blogImageService;
        }

        [HttpGet]
        public IActionResult Image(string id = "", string name ="")
        {
            var blogImage = _blogImageService.GetBlogIamge(id, name);
            return File(blogImage.BinaryData, "image/jpeg");
        }

        public IActionResult Index(string id = "", string name = "")
        {
            return Content("Active");
        }
    }
}