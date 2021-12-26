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
        public ActionResult MovieDetail(string id)
        {
            CustomModel custom = new CustomModel();

            int _id = Convert.ToInt32(id);
            int _serieId = Convert.ToInt32(Request["serieId"]);

            var _movie = new Movie();
            var _list_episode = new List<Episode>();
            var _categoryformovies = new List<CategoryForMovies>();
            var _list_movie = new List<Movie>();
            var _episode = new Episode();
            var _movie_rate = new List<MovieRate>();

            _movie = db.Movie.Find(_id);
            _list_episode = db.Episode.Where(x => x.movieId == _id).ToList();
            _categoryformovies = db.CategoryForMovies.Where(x => x.movieId == _id).ToList();
            _list_movie = db.Movie.Where(x => x.seriesId == _serieId).ToList();
            _episode = db.Episode.Where(x=>x.movieId==_id).FirstOrDefault();
            _movie_rate = db.MovieRate.Where(x => x.movieId == _id).ToList();

            custom.movie = _movie;
            custom.list_episode = _list_episode;
            custom.list_categoryformovies = _categoryformovies;
            custom.list_movie = _list_movie;
            custom.episode = _episode;
            custom.movie_rate = _movie_rate;

            return View(custom);
        }
        public ActionResult MovieEpisode(string id)
        {
            CustomModel custom = new CustomModel();

            int _id = Convert.ToInt32(id);
            int episode_option = Convert.ToInt32(Request["episode_option"]);
            

            var _movie = new Movie();
            var _episode_option = new Episode();
            var _list_episode = new List<Episode>();
            var _account = new Account();

            _movie = db.Movie.Find(_id);
            _episode_option = db.Episode.Find(episode_option);
            _list_episode = db.Episode.Where(x => x.movieId == _id).ToList();
            _account = db.Account.Find(1);

            custom.movie = _movie;
            custom.episode = _episode_option;
            custom.list_episode = _list_episode;
            custom.account = _account;
            return View(custom);
        }

        public ActionResult _load_comment(int id)
        {
            int _id = id;
            var date_comment = new List<Object>();
            var _load_comment = db.Comment.Select(x => new { id = x.id,
                movieId = x.movieId, movieName = x.Movie.name,
                displayName = x.Account.displayName,imageAccount=x.Account.image,
                content = x.content, commentDate =x.commentDate, fatherComment = x.fatherComment })
                .Where(x => x.movieId == _id).OrderByDescending(x=>x.commentDate).ToList();

            foreach(var item in _load_comment)
            {
               
                DateTime comment_date = Convert.ToDateTime(item.commentDate);
                int year = comment_date.Year;
                int month = comment_date.Month;
                int day = comment_date.Day;
                int hour = comment_date.Hour;
                int minute = comment_date.Minute;
                int second = comment_date.Second;

                int comment_date_Second = second + minute * 60  +  hour*3600 + day*86400 + month* 2592000 + year * 31536000;

                DateTime Today = Convert.ToDateTime(DateTime.Now);
                int yearNow = Today.Year;
                int monthNow = Today.Month;
                int dayNow = Today.Day;
                int hourNow = Today.Hour;
                int minuteNow = Today.Minute;
                int secondNow = Today.Second;

                int today_second = secondNow + minuteNow*60 + hourNow*3600 + dayNow*86400 + monthNow * 2592000 + yearNow*31536000;

                int result_second = today_second- comment_date_Second;
                            
                date_comment.Add(new { id = item.id,
                    movieId = item.movieId,movieName=item.movieName,
                    displayName =item.displayName,imageAccount=item.imageAccount,content=item.content,commentDate= result_second,
                    fatherComment=item.fatherComment });
            }
            return Json(date_comment, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Add_commentFather(int id, string comment)
        {
            int movie_id = Convert.ToInt32(id);

            string _comment = comment;
            DateTime dt = DateTime.Now;

            Comment cm = new Comment() { movieId = movie_id, accountId = 1, content = _comment ,commentDate= dt };

            db.Comment.Add(cm);
            db.SaveChanges();

            return Json("Thành công", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RatingMovie(string id)
        {
            int _id = Convert.ToInt32(id);
            int rating = Convert.ToInt32(Request["rating"]);
            var check =db.MovieRate.Where(x=>x.accountId==1).FirstOrDefault();
            if (check == null)
            {
                MovieRate mr = new MovieRate() { movieId = _id, accountId = 1, rateNumber=rating };

                db.MovieRate.Add(mr);
                db.SaveChanges();
            }
            else
            {
                check.rateNumber= rating;
                db.SaveChanges();
            }
            return  Redirect(Request.UrlReferrer.ToString()); ;
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