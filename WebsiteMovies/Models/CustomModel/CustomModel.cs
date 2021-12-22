using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteMovies.Models;
namespace WebsiteMovies.Models.CustomModel
{
    public class CustomModel
    {
        public Movie movie { get; set; }
        public List<Movie> list_movie { get; set; }
        public List<Episode> list_episode { get; set; }
        public Episode episode { get; set; }
        public List<CategoryForMovies> list_categoryformovies { get; set; }
    }
}