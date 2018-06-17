﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTOs
{
    public class BlogImageDto
    {
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(512)]
        public string Name { get; set; }
        [StringLength(5)]
        public string Extension { get; set; }
        public byte[] BinaryData { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
