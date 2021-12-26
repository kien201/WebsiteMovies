using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;
namespace WebsiteMovies.Controllers
{
    public class AccountInfoController : Controller
    {
    private MovieEntities db = new MovieEntities();
        // GET: AccountInfo
        public ActionResult Index()
        {
            int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
            return View(db.Account.Find(curUserId));
        }
        public ActionResult PasswordForm()
        {
            int curUserId = Convert.ToInt32((Session["curUser"] as Dictionary<string, string>)["id"]);
            return View(db.Account.Find(curUserId));
        }
    }
}