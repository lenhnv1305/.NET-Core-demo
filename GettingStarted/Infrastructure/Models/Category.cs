using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Category
    {
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(1024)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
