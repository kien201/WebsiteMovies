using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class MovieMetadata
    {
        [Display(Name = "Tên phim")]
        [Required(ErrorMessage = "Tên phim không được để trống!")]
        public string name { get; set; }

        [Display(Name = "Tên khác")]
        [DisplayFormat(NullDisplayText = "??")]
        public string anotherName { get; set; }

        [Display(Name = "Ảnh")]
        public string image { get; set; }

        [Display(Name = "Năm phát hành")]
        [Range(1970, 9999, ErrorMessage = "Giá trị nằm trong khoảng 1970 đến 9999")]
        public Nullable<int> releaseYear { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [Display(Name = "Thời lượng")]
        [Range(1, 9999, ErrorMessage = "Giá trị nằm trong khoảng 1 đến 9999")]
        [DisplayFormat(NullDisplayText = "??")]
        public Nullable<int> duration { get; set; }

        [Display(Name = "Phần")]
        [Range(1, 9999, ErrorMessage = "Giá trị nằm trong khoảng 1 đến 9999")]
        [DisplayFormat(NullDisplayText = "??")]
        public Nullable<int> part { get; set; }
        
        [Display(Name = "Tên hiển thị trong series")]
        [DisplayFormat(NullDisplayText = "??")]
        public string nameInSeries { get; set; }

        [Display(Name = "Mới cập nhật")]
        [DisplayFormat(DataFormatString = "{0:HH:mmp - dd/MM/yyyy}")]
        public Nullable<DateTime> updatedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public Nullable<int> status { get; set; }
    }
}