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
    public class MoviesController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET: Admin/Movies
        public ActionResult Index()
        {
            var movie = db.Movie.OrderByDescending(x => x.updatedDate).Include(m => m.Series);
            return View(movie.ToList());
        }

        // GET: Admin/Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        private List<SelectListItem> GetStatusList()
        {
            var statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem() { Value = "0", Text = "Đang tiến hành" });
            statusList.Add(new SelectListItem() { Value = "1", Text = "Hoàn thành" });
            return statusList;
        }

        // GET: Admin/Movies/Create
        public ActionResult Create()
        {
            ViewBag.seriesId = new SelectList(db.Series.OrderBy(x => x.name), "id", "name");
            ViewBag.status = new SelectList(GetStatusList(), "Value", "Text", 0);
            return View();
        }

        // POST: Admin/Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,anotherName,image,releaseYear,description,duration,seriesId,part,nameInSeries,status")] Movie movie)
        {
            var f = Request.Files["image"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("~/Assets/Images/MovieImages/" + f.FileName);
                f.SaveAs(path);
                movie.image = f.FileName;
            }
            else movie.image = "default-movie-image.png";
            if (ModelState.IsValid)
            {
                movie.updatedDate = DateTime.Now;
                db.Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.seriesId = new SelectList(db.Series.OrderBy(x => x.name), "id", "name", movie.seriesId);
            ViewBag.status = new SelectList(GetStatusList(), "Value", "Text", movie.status);
            return View(movie);
        }

        // GET: Admin/Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.seriesId = new SelectList(db.Series.OrderBy(x => x.name), "id", "name", movie.seriesId);
            ViewBag.status = new SelectList(GetStatusList(), "Value", "Text", movie.status);
            return View(movie);
        }

        // POST: Admin/Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,anotherName,image,releaseYear,description,duration,seriesId,part,nameInSeries,updatedDate,status")] Movie movie, string oldImageName)
        {
            var f = Request.Files["image"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("~/Assets/Images/MovieImages/" + f.FileName);
                f.SaveAs(path);
                movie.image = f.FileName;
            }
            else movie.image = oldImageName;
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.seriesId = new SelectList(db.Series.OrderBy(x => x.name), "id", "name", movie.seriesId);
            ViewBag.status = new SelectList(GetStatusList(), "Value", "Text", movie.status);
            return View(movie);
        }

        // GET: Admin/Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Admin/Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
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
