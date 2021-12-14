using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class SeriesMetadata
    {
        [Display(Name = "Tên serie")]
        [Required(ErrorMessage = "Không để trống!")]
        public string name { get; set; }
    }
}