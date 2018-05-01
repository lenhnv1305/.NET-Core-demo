using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.ManageBlogModels
{
    public class BlogViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}
