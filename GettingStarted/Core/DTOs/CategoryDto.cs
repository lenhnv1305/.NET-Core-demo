using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTOs
{
    public class CategoryDto
    {
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(1024)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
    }
}
