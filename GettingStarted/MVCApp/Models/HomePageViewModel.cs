using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<CategoryViewModels.CategoryViewModel> Categories { get; set; }
        public IEnumerable<PostViewModels.PostViewModel> Posts { get; set; }
    }
}
