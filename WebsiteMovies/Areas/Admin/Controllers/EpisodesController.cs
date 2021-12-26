using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;

namespace WebsiteMovies.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EpisodesController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET: Admin/Episodes?idMovie=5
        public ActionResult Index(int? idMovie)
        {
            if (idMovie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var episode = db.Episode.Where(x => x.movieId == idMovie).OrderBy(x => x.episodeNumber).Include(e => e.Movie);
            ViewBag.idMovie = idMovie;
            ViewBag.movieName = db.Movie.Find(idMovie).name.ToString();
            return View(episode.ToList());
        }

        // GET: Admin/Episodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episode episode = db.Episode.Find(id);
            if (episode == null)
            {
                return HttpNotFound();
            }
            return View(episode);
        }

        // GET: Admin/Episodes/Create?idMovie=5
        public ActionResult Create(int? idMovie)
        {
            if (idMovie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idMovie = idMovie;
            return View();
        }

        // POST: Admin/Episodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,episodeNumber,episodeName,movieId,video")] Episode episode)
        {
            var f = Request.Files["video"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("~/Assets/Videos/" + f.FileName);
                f.SaveAs(path);
                episode.video = f.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Episode.Add(episode);
                db.Movie.Find(episode.movieId).updatedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", new { idMovie = episode.movieId });
            }

            ViewBag.idMovie = episode.movieId;
            return View(episode);
        }

        // GET: Admin/Episodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episode episode = db.Episode.Find(id);
            if (episode == null)
            {
                return HttpNotFound();
            }
            ViewBag.movieId = new SelectList(db.Movie.OrderBy(x => x.name), "id", "name", episode.movieId);
            return View(episode);
        }

        // POST: Admin/Episodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,episodeNumber,episodeName,movieId,video")] Episode episode, string oldVideoName)
        {
            var f = Request.Files["video"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("~/Assets/Videos/" + f.FileName);
                f.SaveAs(path);
                episode.video = f.FileName;
            }
            else episode.video = oldVideoName;
            if (ModelState.IsValid)
            {
                db.Entry(episode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { idMovie = episode.movieId });
            }
            ViewBag.movieId = new SelectList(db.Movie.OrderBy(x => x.name), "id", "name", episode.movieId);
            return View(episode);
        }

        // GET: Admin/Episodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episode episode = db.Episode.Find(id);
            if (episode == null)
            {
                return HttpNotFound();
            }
            return View(episode);
        }

        // POST: Admin/Episodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Episode episode = db.Episode.Find(id);
            int movieId = (int)episode.movieId;
            db.Episode.Remove(episode);
            db.SaveChanges();
            return RedirectToAction("Index", new { idMovie = movieId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
