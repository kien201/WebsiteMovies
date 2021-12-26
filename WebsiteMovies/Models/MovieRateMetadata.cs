using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMovies.Models
{
    public class MovieRateMetadata
    {
        [Required(ErrorMessage = "không để trống!")]
        public Nullable<int> accountId { get; set; }

        [Required(ErrorMessage = "không để trống!")]
        public Nullable<int> movieId { get; set; }

        [Display(Name = "Sao đánh giá")]
        [Required(ErrorMessage = "không để trống!")]
        public Nullable<int> rateNumber { get; set; }
    }
}