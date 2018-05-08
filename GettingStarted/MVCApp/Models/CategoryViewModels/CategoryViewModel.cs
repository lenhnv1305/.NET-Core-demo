using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
    }
}
