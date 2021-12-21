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
    public class CommentsController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comment = db.Comment.Include(c => c.Account).Include(c => c.Comment2).Include(c => c.Movie);
            return View(comment.ToList());
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.accountId = new SelectList(db.Account, "id", "displayName");
            ViewBag.fatherComment = new SelectList(db.Comment, "id", "content");
            ViewBag.movieId = new SelectList(db.Movie, "id", "name");
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,movieId,accountId,content,commentDate,fatherComment")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountId = new SelectList(db.Account, "id", "displayName", comment.accountId);
            ViewBag.fatherComment = new SelectList(db.Comment, "id", "content", comment.fatherComment);
            ViewBag.movieId = new SelectList(db.Movie, "id", "name", comment.movieId);
            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountId = new SelectList(db.Account, "id", "displayName", comment.accountId);
            ViewBag.fatherComment = new SelectList(db.Comment, "id", "content", comment.fatherComment);
            ViewBag.movieId = new SelectList(db.Movie, "id", "name", comment.movieId);
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,movieId,accountId,content,commentDate,fatherComment")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountId = new SelectList(db.Account, "id", "displayName", comment.accountId);
            ViewBag.fatherComment = new SelectList(db.Comment, "id", "content", comment.fatherComment);
            ViewBag.movieId = new SelectList(db.Movie, "id", "name", comment.movieId);
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
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
