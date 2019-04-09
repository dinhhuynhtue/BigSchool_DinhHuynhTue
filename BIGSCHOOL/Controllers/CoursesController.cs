using BIGSCHOOL.Models;
using BIGSCHOOL.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIGSCHOOL.Controllers
{
    public class CoursesController : Controller
    {
       
        private readonly ApplicationDbContext dbContext;

        public CoursesController ()
        {
            dbContext = new ApplicationDbContext();
        }

     

        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
         
        // GET: Coursesggg
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var cours = new Course
            {
                LecturerId= User.Identity.GetUserId(),
                DateTime=viewModel.GetDateTime(),
                CategoryId=viewModel.Category,
                Place=viewModel.Place
            };
            dbContext.Courses.Add(cours);
            dbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}