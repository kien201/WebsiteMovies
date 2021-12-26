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
    public class MovieRatesController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET: Admin/MovieRates
        public ActionResult Index()
        {
            var movieRate = db.MovieRate.Include(m => m.Account).Include(m => m.Movie);
            return View(movieRate.ToList());
        }

        // GET: Admin/MovieRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieRate movieRate = db.MovieRate.Find(id);
            if (movieRate == null)
            {
                return HttpNotFound();
            }
            return View(movieRate);
        }

        // GET: Admin/MovieRates/Create
        public ActionResult Create()
        {
            ViewBag.accountId = new SelectList(db.Account, "id", "displayName");
            ViewBag.movieId = new SelectList(db.Movie, "id", "name");
            return View();
        }

        // POST: Admin/MovieRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,accountId,movieId,rateNumber")] MovieRate movieRate)
        {
            if (ModelState.IsValid)
            {
                db.MovieRate.Add(movieRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountId = new SelectList(db.Account, "id", "displayName", movieRate.accountId);
            ViewBag.movieId = new SelectList(db.Movie, "id", "name", movieRate.movieId);
            return View(movieRate);
        }

        // GET: Admin/MovieRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieRate movieRate = db.MovieRate.Find(id);
            if (movieRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountId = new SelectList(db.Account, "id", "displayName", movieRate.accountId);
            ViewBag.movieId = new SelectList(db.Movie, "id", "name", movieRate.movieId);
            return View(movieRate);
        }

        // POST: Admin/MovieRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,accountId,movieId,rateNumber")] MovieRate movieRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountId = new SelectList(db.Account, "id", "displayName", movieRate.accountId);
            ViewBag.movieId = new SelectList(db.Movie, "id", "name", movieRate.movieId);
            return View(movieRate);
        }

        // GET: Admin/MovieRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieRate movieRate = db.MovieRate.Find(id);
            if (movieRate == null)
            {
                return HttpNotFound();
            }
            return View(movieRate);
        }

        // POST: Admin/MovieRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieRate movieRate = db.MovieRate.Find(id);
            db.MovieRate.Remove(movieRate);
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
