﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}