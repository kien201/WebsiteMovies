using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;
using WebsiteMovies.Models.AdditionalDTO;

namespace WebsiteMovies.Controllers
{
    public class MoviesController : Controller
    {
        private MovieEntities db = new MovieEntities();
        // GET: Movies
        public ActionResult Index(int page = 1)
        {
            var listMovie = db.Movie
                .Select(x => new MovieInfo
                {
                    movie = x,
                    score = (float)(db.MovieRate.Where(r => r.movieId == x.id)
                            .Average(s => s.rateNumber) ?? 0)
                })
                .ToList();
            ViewBag.currentPage = page;
            ViewBag.totalMovieInPage = 30;
            int lastPage = listMovie.Count() / ViewBag.totalMovieInPage + 1;
            if (page > lastPage) return RedirectToAction("Notify", "Home", new { msg = "Giới hạn là " + lastPage + " trang." });
            return View(listMovie);
        }

        [ChildActionOnly]
        public ActionResult RenderListMovie(int? id, IEnumerable<MovieInfo> listMovie, string sendAction, int totalMovieInPage, int currentPage = 1)
        {
            ViewBag.routeId = id;
            //pagination
            ViewBag.lastPage = listMovie.Count() / totalMovieInPage + 1;
            ViewBag.currentPage = currentPage;
            ViewBag.sendAction = sendAction;
            listMovie = listMovie.OrderByDescending(x => x.movie.updatedDate).Skip((currentPage - 1) * 30).Take(30);
            return PartialView("_MovieList", listMovie);
        }

        public ActionResult Category(int? id, int page = 1)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var curCategory = db.Category.SingleOrDefault(x => x.id == id);
            if (curCategory == null) return RedirectToAction("Notify", "Home", new { msg = "Hiện website không có thể loại phim này" });
            ViewBag.curCategory = curCategory;

            var listMovie = db.CategoryForMovies.Where(x => x.categoryId == id)
                .Select(x => new MovieInfo
                {
                    movie = x.Movie,
                    score = (float)(db.MovieRate.Where(r => r.movieId == x.movieId)
                            .Average(s => s.rateNumber) ?? 0)
                })
                .ToList();
            ViewBag.currentPage = page;
            ViewBag.totalMovieInPage = 30;
            int lastPage = listMovie.Count() / ViewBag.totalMovieInPage + 1;
            if (page > lastPage) return RedirectToAction("Notify", "Home", new { msg = "Giới hạn là " + lastPage + " trang." });
            return View(listMovie);
        }

        public ActionResult ReleaseYear(int? id, int page = 1)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.releaseYear = id;
            var listMovie = db.Movie.Where(x => x.releaseYear == id)
                .Select(x => new MovieInfo
                {
                    movie = x,
                    score = (float)(db.MovieRate.Where(r => r.movieId == x.id)
                            .Average(s => s.rateNumber) ?? 0)
                })
                .ToList();
            ViewBag.currentPage = page;
            ViewBag.totalMovieInPage = 30;
            int lastPage = listMovie.Count() / ViewBag.totalMovieInPage + 1;
            if (page > lastPage) return RedirectToAction("Notify", "Home", new { msg = "Giới hạn là " + lastPage + " trang." });
            return View(listMovie);
        }

        //render for layout
        public ActionResult RenderReleaseYear()
        {
            return PartialView("_RenderReleaseYear", db.Movie
                .GroupBy(x => x.releaseYear)
                .OrderByDescending(x => x.Key)
                .Select(x => (int)x.Key).ToList());
        }
    }
}