using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class CategoryMetadata
    {
        [Display(Name = "Tên thể loại hiển thị")]
        [Required(ErrorMessage = "Không để trống!")]
        public string name { get; set; }
    }
}