using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMovies.Models;

namespace WebsiteMovies.Controllers
{
    public class CategoriesController : Controller
    {
        private MovieEntities db = new MovieEntities();
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        // Render category list for dropdown header in layout
        public ActionResult RenderCategory()
        {
            return PartialView("_RenderCategory", db.Category.OrderBy(x=>x.name).ToList());
        }
    }
}