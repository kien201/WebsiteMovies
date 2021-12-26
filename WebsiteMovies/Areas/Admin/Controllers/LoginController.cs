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
                if (user == null || user.role == 1) ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
                else
                {
                    FormsAuthentication.SetAuthCookie(user.userName, inputRememberPassword == "on");
                    if (ReturnUrl != null) return Redirect(ReturnUrl);
                    return RedirectToAction("Index", "Movies");
                }
            }
            return View(account);
        }

        public EmptyResult CheckLogin()
        {
            var curUser = db.Account.SingleOrDefault(x => x.userName == User.Identity.Name && x.role == 0);
            if (curUser != null)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("id", curUser.id.ToString());
                dic.Add("displayName", curUser.displayName);
                dic.Add("userName", curUser.userName);
                dic.Add("image", curUser.image);
                Session.Add("curUser", dic);
            }
            return new EmptyResult();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("curUser");
            return RedirectToAction("Index");
        }
    }
}