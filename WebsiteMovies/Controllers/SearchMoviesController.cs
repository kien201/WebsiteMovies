using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;
using WebsiteMovies.Models.AdditionalDTO;

namespace WebsiteMovies.Controllers
{
    public class SearchMoviesController : Controller
    {
        private MovieEntities db = new MovieEntities();
        public ActionResult Index(string movieName, int page = 1)
        {
            var moviesByName = db.Movie.Where(x => x.name.Contains(movieName) || x.anotherName.Contains(movieName));
            //pagination
            int lastPage = (moviesByName.Count() / 30 + 1);
            if (page > lastPage) return RedirectToAction("Notify", "Home", new { msg = "Giới hạn là " + lastPage + " trang." });
            ViewBag.lastPage = lastPage;
            ViewBag.currentPage = page;
            ViewBag.movieName = movieName;

            var listMovie = moviesByName
                .OrderByDescending(x => x.updatedDate).Skip((page - 1) * 30).Take(30)
                .Select(x => new MovieInfo
                {
                    movie = x,
                    score = (float)(db.MovieRate.Where(r => r.movieId == x.id)
                            .Average(s => s.rateNumber) ?? 0)
                })
                .ToList();
            return View(listMovie);
        }

        [ChildActionOnly]
        public ActionResult Pagination(string movieName, string sendAction, int lastPage, int currentPage = 1)
        {
            ViewBag.lastPage = lastPage;
            ViewBag.currentPage = currentPage;
            ViewBag.sendAction = sendAction;
            ViewBag.movieName = movieName;
            return PartialView("_Pagination");
        }

        public JsonResult GetMoviesByName(string name)
        {
            var listMovie = db.Movie.Where(x => x.name.Contains(name) || x.anotherName.Contains(name))
                .OrderByDescending(x => x.updatedDate).Take(10)
                .Select(x => new
                {
                    x.id,
                    x.image,
                    x.name,
                    episode = x.Episode.Count,
                    duration = x.duration.ToString() ?? "??"
                })
                .ToList();
            return Json(listMovie, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Filter()
        {
            return View();
        }
    }
}