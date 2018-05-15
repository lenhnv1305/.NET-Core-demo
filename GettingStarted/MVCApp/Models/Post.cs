using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class Post
    {
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(256)]
        public string CatedoryId { get; set; }
        [StringLength(1024)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string ShortDescription { get; set; }
        [StringLength(4000)]
        public string Content { get; set; }
        [StringLength(1024)]
        public string ThumbnailImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Category Category { get; set; }
    }
}
