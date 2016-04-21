using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPSBlog.Models
{
    public class CommentViewModel
    {
        public int BlogId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="This field is required")]
        [MinLength(10, ErrorMessage ="Min 10 chars")]
        [MaxLength(140, ErrorMessage ="You've exceeded 140 char limit")]
        public string Body { get; set; }
    }
}