using BIGSCHOOL.Models;
using BIGSCHOOL.ViewModels;
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
       
        private readonly ApplicationDbContext _dbContext;
        
         public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public List<Category> Categories { get; private set; }


        // GET: Coursesggg
        [Authorize]
        [HttpPost]
        public ActionResult Create(CourseViewModel viewModel )
        {
            var course = new Course
            {
                LecturerId= User.Identity.GetUserId(),
                DateTime=viewModel.GetDateTime(),
                CategoryId=viewModel.Category,
                Place=viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Create","Courses");
        }
    }
}