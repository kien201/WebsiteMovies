using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteMovies.Models;

namespace WebsiteMovies.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Admin/Login/CheckLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account account, string inputRememberPassword, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                string md5pass = Code.Md5hash.md5(account.pass);
                var user = db.Account.FirstOrDefault(x => x.userName == account.userName && x.pass == md5pass);
                if (user == null) ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
                else
                {
                    Session["userName"] = user.userName;
                    bool isRememberPass = inputRememberPassword == "on";
                    FormsAuthentication.SetAuthCookie(user.userName, isRememberPass);
                    if (ReturnUrl != null) return Redirect(ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}