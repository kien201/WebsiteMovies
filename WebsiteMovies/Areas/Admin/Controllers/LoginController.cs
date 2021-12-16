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
                else if (user.role == 1) ModelState.AddModelError("", "Tài khoản không đủ quyền");
                else
                {
                    if(inputRememberPassword == "on")
                    {
                        FormsAuthentication.SetAuthCookie(user.userName, true);
                        Response.Cookies.Add(new HttpCookie("curUserName", user.userName) { Expires = DateTime.Now.AddMonths(4) });
                        Response.Cookies.Add(new HttpCookie("curDisplayName", user.displayName) { Expires = DateTime.Now.AddMonths(4) });
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.userName, false);
                        Response.Cookies.Add(new HttpCookie("curUserName", user.userName));
                        Response.Cookies.Add(new HttpCookie("curDisplayName", user.displayName));
                    }
                    if (ReturnUrl != null) return Redirect(ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove("curUserName");
            Response.Cookies.Remove("curDisplayName");
            return RedirectToAction("Index");
        }
    }
}