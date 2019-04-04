using BIGSCHOOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BIGSCHOOL.Controllers
{
    public class HomeController : Controller
    {
        private  ApplicationDbContext dbContext;

        public HomeController()
        {
            dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingCourses = dbContext.Courses.Include(c => c.Lecturer ).Include(c => c.Category).Where(c => c.DateTime > DateTime.Now);   
            return View(upcomingCourses);
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