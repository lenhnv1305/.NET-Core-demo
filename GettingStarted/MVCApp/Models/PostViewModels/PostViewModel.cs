﻿using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MVCApp.Extensions;

namespace MVCApp.Models.PostViewModels
{
    public class PostViewModel
    {
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(256)]
        [Display(Name = "Category Name")]
        public string CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [StringLength(1024)]        
        public string Title { get; set; }

        [StringLength(4000)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [StringLength(4000)]
        public string Content { get; set; }

        [StringLength(1024)]
        [Display(Name = "Thumbnail Image")]
        public string ThumbnailImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [StringLength(256)]
        public string OwnerId { get; set; }
        [StringLength(512)]
        public string Slug { get; set; }
        public string ImageId
        {
            get
            {
                if (string.IsNullOrEmpty(this.ThumbnailImage))
                {
                    return "";
                }
                return this.ThumbnailImage.Split("__")[1];
            }
        }
    }
}
