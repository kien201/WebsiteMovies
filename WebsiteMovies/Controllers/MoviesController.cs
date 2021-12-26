using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;
using WebsiteMovies.Models.AdditionalDTO;
using WebsiteMovies.Models.CustomModel;

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

        public ActionResult MovieDetail(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CustomModel custom = new CustomModel();
            var _movie = db.Movie.Find(id);
            var _list_episode = db.Episode.Where(x => x.movieId == id).ToList();
            var _categoryformovies = db.CategoryForMovies.Where(x => x.movieId == id).ToList();
            var _list_movie = db.Movie.Where(x => x.seriesId == _movie.seriesId && x.seriesId != null).ToList();
            var _episode = db.Episode.Where(x => x.movieId == id).FirstOrDefault();
            var _movie_rate = db.MovieRate.Where(x => x.movieId == id).ToList();

            custom.movie = _movie;
            custom.list_episode = _list_episode;
            custom.list_categoryformovies = _categoryformovies;
            custom.list_movie = _list_movie;
            custom.episode = _episode;
            custom.movie_rate = _movie_rate;

            return View(custom);
        }

        public ActionResult FollowMovie(int? id)
        {
            if (Session["curUser"] == null)
            {
                return Json("Bạn phải đăng nhập để theo dõi phim", JsonRequestBehavior.AllowGet);
            }
            int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
            var movie = db.Movie.Where(x => x.id == id).SingleOrDefault();
            if (movie == null)
            {
                return Json("Không tìm thấy phim", JsonRequestBehavior.AllowGet);
            }
            var follow = db.Follow.Where(x => x.accountId == curUserId && x.movieId == movie.id).FirstOrDefault();
            if (follow != null)
            {
                db.Follow.Remove(follow);
                db.SaveChanges();
                return Json("Bỏ theo dõi phim " + movie.name + " thành công", JsonRequestBehavior.AllowGet);
            }
            db.Follow.Add(new Follow() { accountId = curUserId, movieId = movie.id });
            db.SaveChanges();
            return Json("Theo dõi phim " + movie.name + " thành công", JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateViews(int idMovie)
        {
            var dateView = db.ViewsByDate.Where(x => x.movieId == idMovie && x.day == DateTime.Now.Date).SingleOrDefault();
            if(dateView != null)
            {
                dateView.numView += 1;
                db.SaveChanges();
            }
            else
            {
                db.ViewsByDate.Add(new ViewsByDate() { day = DateTime.Now.Date, movieId = idMovie, numView = 1 });
                db.SaveChanges();
            }
            return Json("Cập nhật thành công", JsonRequestBehavior.AllowGet);
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

            if (Session["curUser"] != null)
            {
                int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
                _account = db.Account.Find(curUserId);
                var history = db.History.Where(x => x.accountId == curUserId && x.Episode.movieId == _id).FirstOrDefault();
                if (history != null)
                {
                    history.episodeId = episode_option;
                    db.SaveChanges();
                }
                else
                {
                    db.History.Add(new History() { accountId = curUserId, episodeId = episode_option });
                    db.SaveChanges();
                }
            }

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
            var _load_comment = db.Comment.Select(x => new {
                id = x.id,
                movieId = x.movieId,
                movieName = x.Movie.name,
                displayName = x.Account.displayName,
                imageAccount = x.Account.image,
                content = x.content,
                commentDate = x.commentDate,
                fatherComment = x.fatherComment
            })
                .Where(x => x.movieId == _id).OrderByDescending(x => x.commentDate).ToList();

            foreach (var item in _load_comment)
            {

                DateTime comment_date = Convert.ToDateTime(item.commentDate);
                int year = comment_date.Year;
                int month = comment_date.Month;
                int day = comment_date.Day;
                int hour = comment_date.Hour;
                int minute = comment_date.Minute;
                int second = comment_date.Second;

                int comment_date_Second = second + minute * 60 + hour * 3600 + day * 86400 + month * 2592000 + year * 31536000;

                DateTime Today = Convert.ToDateTime(DateTime.Now);
                int yearNow = Today.Year;
                int monthNow = Today.Month;
                int dayNow = Today.Day;
                int hourNow = Today.Hour;
                int minuteNow = Today.Minute;
                int secondNow = Today.Second;

                int today_second = secondNow + minuteNow * 60 + hourNow * 3600 + dayNow * 86400 + monthNow * 2592000 + yearNow * 31536000;

                int result_second = today_second - comment_date_Second;

                date_comment.Add(new
                {
                    id = item.id,
                    movieId = item.movieId,
                    movieName = item.movieName,
                    displayName = item.displayName,
                    imageAccount = item.imageAccount,
                    content = item.content,
                    commentDate = result_second,
                    fatherComment = item.fatherComment
                });
            }
            return Json(date_comment, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Add_commentFather(int id, string comment)
        {
            if(Session["curUser"] == null)
            {
                return Json("Bạn phải đăng nhập để bình luận", JsonRequestBehavior.AllowGet);
            }
            int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
            int movie_id = Convert.ToInt32(id);
            string _comment = comment;
            DateTime dt = DateTime.Now;

            Comment cm = new Comment() { movieId = movie_id, accountId = curUserId, content = _comment, commentDate = dt };

            db.Comment.Add(cm);
            db.SaveChanges();

            return Json("Thành công", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RatingMovie(string id)
        {
            if (Session["curUser"] == null)
            {
                return RedirectToAction("Notify", "Home", new { msg = "Bạn phải đăng nhập để đánh giá" });
            }
            int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
            int _id = Convert.ToInt32(id);
            int rating = Convert.ToInt32(Request["rating"]);
            var check = db.MovieRate.Where(x => x.accountId == curUserId && x.movieId == _id).FirstOrDefault();
            if (check == null)
            {
                MovieRate mr = new MovieRate() { movieId = _id, accountId = curUserId, rateNumber = rating };
                db.MovieRate.Add(mr);
                db.SaveChanges();
            }
            else
            {
                check.rateNumber = rating;
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
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