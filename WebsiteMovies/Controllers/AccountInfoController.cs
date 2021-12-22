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

            return View(db.Account.Find(1));
        }
        public ActionResult PasswordForm()
        {
            return View(db.Account.Find(1));
        }
    }
}