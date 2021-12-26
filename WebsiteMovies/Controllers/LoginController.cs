using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteMovies.Models;

namespace WebsiteMovies.Controllers
{
    public class LoginController : Controller
    {
        private MovieEntities db = new MovieEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account account, string inputRememberPassword)
        {
            if (ModelState.IsValid)
            {
                string md5pass = Areas.Admin.Code.Md5hash.md5(account.pass);
                var user = db.Account.FirstOrDefault(x => x.userName == account.userName && x.pass == md5pass);
                if (user == null) ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
                else
                {
                    FormsAuthentication.SetAuthCookie(user.userName, inputRememberPassword == "on");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(account);
        }

        public EmptyResult CheckLogin()
        {
            var curUser = db.Account.SingleOrDefault(x => x.userName == User.Identity.Name);
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account account, string retypePass)
        {
            if(account.pass != retypePass) ModelState.AddModelError("", "Mật khẩu nhập lại không đúng");
            int count = db.Account.Where(x => x.userName == account.userName).Count();
            if (count > 0) ModelState.AddModelError("", "Tài khoản đã có người đăng ký");
            if (ModelState.IsValid)
            {
                account.pass = Areas.Admin.Code.Md5hash.md5(account.pass);
                account.role = 1;
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Notify", "Home", new { msg = "Bạn đã đăng ký tài khoản thành công" });
            }
            return View(account);
        }
    }
}