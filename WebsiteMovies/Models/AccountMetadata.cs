using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class AccountMetadata
    {
        [Display(Name = "Tên hiển thị")]
        public string displayName { get; set; }

        [Display(Name = "Ảnh")]
        public string image { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Tài khoản là bắt buộc!")]
        public string userName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc!")]
        [MinLength(3, ErrorMessage = "Mật khẩu phải có tối thiểu 3 ký tự!")]
        public string pass { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email là bắt buộc!")]
        [EmailAddress(ErrorMessage = "Không đúng định dạng email!")]
        public string email { get; set; }

        [Display(Name = "Chức vụ")]
        public Nullable<int> role { get; set; }
    }
}