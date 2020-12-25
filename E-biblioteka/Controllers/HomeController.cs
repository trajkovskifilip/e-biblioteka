using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_biblioteka.Models;

namespace E_biblioteka.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public HomeController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public ActionResult Index()
        {
            return View(db.Query<Book>().Include(b => b.Author).OrderByDescending(book => book.Rating).ToList().GetRange(0, 5));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}