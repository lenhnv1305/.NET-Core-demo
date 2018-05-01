using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Models.ManageBlogModels;

namespace MVCApp.Controllers
{
    [Route("[controller]/[action]")]
    public class ManageBlogController : Controller
    {
        private readonly Data.ApplicationDbContext _context;
        public ManageBlogController(Data.ApplicationDbContext  context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Blog", new BlogViewModel());
        }

        [HttpPost]
        public IActionResult Create(BlogViewModel model, string returnUrl = null)
        {
            return Content("Successfully");
        }
    }
}