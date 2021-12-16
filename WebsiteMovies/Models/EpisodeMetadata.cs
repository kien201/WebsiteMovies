using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class EpisodeMetadata
    {
        [Display(Name = "Thứ tự tập phim")]
        [Required(ErrorMessage = "Thứ tự tập phim không được để trống!")]
        [Range(1, 9999, ErrorMessage = "Giá trị nằm trong khoảng 1 đến 9999")]
        public Nullable<int> episodeNumber { get; set; }

        [Display(Name = "Tên tập phim")]
        [DisplayFormat(NullDisplayText = "??")]
        public string episodeName { get; set; }

        [Display(Name = "Video")]
        [DisplayFormat(NullDisplayText = "??")]
        public string video { get; set; }
    }
}