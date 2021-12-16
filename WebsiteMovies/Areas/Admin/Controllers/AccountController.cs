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
    public class AccountController : Controller
    {
        private MovieEntities db = new MovieEntities();
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View(db.Account.OrderBy(x => x.displayName).ToList());
        }

        // GET: Admin/Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        private List<SelectListItem> GetRoleList()
        {
            var roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem() { Value = "0", Text = "Admin" });
            roleList.Add(new SelectListItem() { Value = "1", Text = "Thành viên" });
            return roleList;
        }

        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            ViewBag.roleList = new SelectList(GetRoleList(), "Value", "Text", 1);
            return View();
        }

        // POST: Admin/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,displayName,userName,pass,email,role")] Account account)
        {
            int count = db.Account.Where(x => x.userName == account.userName).Count();
            if (count > 0) ModelState.AddModelError("", "Tài khoản đã có người đăng ký");
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roleList = new SelectList(GetRoleList(), "Value", "Text", account.role);
            return View(account);
        }

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.roleList = new SelectList(GetRoleList(), "Value", "Text", account.role);
            return View(account);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,displayName,userName,pass,email,role")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roleList = new SelectList(GetRoleList(), "Value", "Text", account.role);
            return View(account);
        }

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Account.Find(id);
            db.Account.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Account/ResetPass/5
        public ActionResult ResetPass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Account/ResetPass/5
        [HttpPost, ActionName("ResetPass")]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassConfirmed(int id)
        {
            Account account = db.Account.Find(id);
            account.pass = Code.Md5hash.md5("123");
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
