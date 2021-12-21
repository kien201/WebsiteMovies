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
        public ActionResult MovieDetail(int id)
        {
            CustomModel custom = new CustomModel();

            int _id = Convert.ToInt32(id);
            var _movie = new Movie();
            var _list_episode = new List<Episode>();
            var _categoryformovies = new List<CategoryForMovies>();

            _movie = db.Movie.Find(_id);
            _list_episode = db.Episode.Where(x => x.movieId == _id).ToList();
            _categoryformovies = db.CategoryForMovies.Where(x => x.movieId == id).ToList();

            custom.movie = _movie;
            custom.list_episode = _list_episode;
            custom.list_categoryformovies = _categoryformovies;

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
            var _load_comment = db.Comment.Select(x => new { id = x.id, movieId = x.movieId, movieName = x.Movie.name, displayName = x.Account.displayName, content = x.content, commentDate = x.commentDate, fatherComment = x.fatherComment }).Where(x => x.movieId == _id).ToList();

            return Json(_load_comment, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Add_commentFather(int id, string comment)
        {
            int movie_id = Convert.ToInt32(id);

            string _comment = comment;
            var time = DateTime.Now;
            Comment cm = new Comment() { movieId = movie_id, accountId = 1, content = _comment, commentDate = time };
            db.Comment.Add(cm);
            db.SaveChanges();
            return Json("thành công", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add_commentChild(int id, int id_commentFather, string comment)
        {
            int movie_id = Convert.ToInt32(id);
            int _idCommentFather = Convert.ToInt32(id_commentFather);
            string _comment = comment;
            var time = DateTime.Now;
            Comment cm = new Comment() { movieId = movie_id, accountId = 1, content = _comment, commentDate = time, fatherComment = _idCommentFather };
            db.Comment.Add(cm);
            db.SaveChanges();
            return Json("Thành công", JsonRequestBehavior.AllowGet);
        }
    }
}