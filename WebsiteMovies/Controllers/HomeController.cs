using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;
using WebsiteMovies.Models.CustomModel;
namespace WebsiteMovies.Controllers
{
    public class HomeController : Controller
    {
        private MovieEntities db = new MovieEntities();
        public ActionResult Index()
        {

            return View(db.Movie.ToList());
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


            _movie = db.Movie.Find(_id);
            _list_episode = db.Episode.Where(x => x.movieId == _id).ToList();
            _categoryformovies = db.CategoryForMovies.Where(x => x.movieId == _id).ToList();
            _list_movie = db.Movie.Where(x => x.seriesId == _serieId).ToList();
            _episode = db.Episode.Where(x=>x.movieId==_id).FirstOrDefault();

            custom.movie = _movie;
            custom.list_episode = _list_episode;
            custom.list_categoryformovies = _categoryformovies;
            custom.list_movie = _list_movie;
            custom.episode = _episode;

            return View(custom);
        }
        public ActionResult MovieEpisode(string id)
        {
            CustomModel custom = new CustomModel();

            int _id = Convert.ToInt32(id);
            int _episodeIsplaying = Convert.ToInt32(Request["episode_option"]);
            

            var _movie = new Movie();
            var _episode_playing = new Episode();
            var _list_episode = new List<Episode>();

            _movie = db.Movie.Find(_id);
            _episode_playing = db.Episode.Find(_episodeIsplaying);
            _list_episode = db.Episode.Where(x => x.movieId == _id).ToList();

            custom.movie = _movie;
            custom.episode = _episode_playing;
            custom.list_episode = _list_episode;
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
                .Where(x => x.movieId == _id).OrderBy(x=>x.commentDate).ToList();
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
            string ketqua = dt.ToString("dd");
            Comment cm = new Comment() { movieId = movie_id, accountId = 1, content = _comment ,commentDate= dt };
            db.Comment.Add(cm);
            db.SaveChanges();
            return Json(ketqua, JsonRequestBehavior.AllowGet);
        }
    }
}