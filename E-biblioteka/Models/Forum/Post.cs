﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_biblioteka.Models.Forum
{
    public class Post
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Title length must not exceed 64 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Content length must not exceed 500 characters")]
        public string Content { get; set; }

        public virtual Member User { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<Comment> Comments{ get; set; }
    }
}