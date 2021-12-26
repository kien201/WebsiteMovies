using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;
using WebsiteMovies.Models.CustomModel;
using WebsiteMovies.Models.AdditionalDTO;

namespace WebsiteMovies.Controllers
{
    public class HomeController : Controller
    {
        private MovieEntities db = new MovieEntities();
        public ActionResult Index()
        {
            var listMovie = db.Movie
                .Select(x => new MovieInfo
                {
                    movie = x,
                    score = (float)(db.MovieRate.Where(r => r.movieId == x.id)
                            .Average(s => s.rateNumber) ?? 0)
                })
                .ToList();
            ViewBag.totalMovieInPage = 30;
            ViewBag.listMovie = listMovie;

            var sliderListMovie = db.Movie
                .Select(x => new
                {
                    movie = x,
                    views = db.ViewsByDate
                        .Where(y => y.movieId == x.id && DbFunctions.DiffDays(DateTime.Now, y.day) <= 7)
                        .Sum(y => y.numView) ?? 0
                })
                .OrderByDescending(x => x.views)
                .ThenByDescending(x => x.movie.updatedDate)
                .Select(x => x.movie).Take(10)
                .ToList();
            return View(sliderListMovie);
        }

        public ActionResult History(int page = 1)
        {
            var historyList = new List<History>();
            if(Session["curUser"] != null)
            {
                int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
                historyList = db.History.Where(x => x.accountId == curUserId).ToList();
                //pagination
                int lastPage = (historyList.Count / 30 + 1);
                if (page > lastPage) return RedirectToAction("Notify", "Home", new { msg = "Giới hạn là " + lastPage + " trang." });
                ViewBag.lastPage = lastPage;
                ViewBag.currentPage = page;
                
                historyList = historyList.OrderByDescending(x=>x.id)
                    .Skip((page - 1) * 30).Take(30)
                    .ToList();
            }
            return View(historyList);
        }

        public ActionResult Follow(int page = 1)
        {
            var movieList = new List<MovieInfo>();
            if (Session["curUser"] != null)
            {
                int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
                var followList = db.Follow.Where(x => x.accountId == curUserId).ToList();
                //pagination
                int lastPage = (followList.Count / 30 + 1);
                if (page > lastPage) return RedirectToAction("Notify", "Home", new { msg = "Giới hạn là " + lastPage + " trang." });
                ViewBag.lastPage = lastPage;
                ViewBag.currentPage = page;

                movieList = followList.OrderByDescending(x => x.id)
                    .Select(x => new MovieInfo
                    {
                        movie = x.Movie,
                        score = (float)(db.MovieRate.Where(r => r.movieId == x.movieId)
                                .Average(s => s.rateNumber) ?? 0)
                                                          
                    })
                    .Skip((page - 1) * 30).Take(30)
                    .ToList();
            }
            return View(movieList);
        }

        [ChildActionOnly]
        public ActionResult Pagination(string sendAction, int lastPage, int currentPage = 1)
        {
            ViewBag.lastPage = lastPage;
            ViewBag.currentPage = currentPage;
            ViewBag.sendAction = sendAction;
            return PartialView("_Pagination");
        }

        public ActionResult Notify(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }
    }
}