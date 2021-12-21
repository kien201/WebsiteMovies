using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class CommentMetadata
    {
        [Display(Name = "id phim bình luận")]
        [Required(ErrorMessage = "Không để trống!")]
        public Nullable<int> movieId { get; set; }

        [Display(Name = "id tài khoản bình luận")]
        [Required(ErrorMessage = "Không để trống!")]
        public Nullable<int> accountId { get; set; }

        [Display(Name = "Bình luận")]
        [Required(ErrorMessage = "Không để trống!")]
        public string content { get; set; }

        [Display(Name = "Ngày bình luận")]
        //[Required(ErrorMessage = "Không để trống!")]
        public Nullable<System.DateTime> commentDate { get; set; }

        [Display(Name = "Phản hồi bình luận")]
        //[Required(ErrorMessage = "Không để trống!")]
        public Nullable<int> fatherComment { get; set; }
    }
}