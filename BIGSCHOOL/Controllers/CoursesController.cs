using BIGSCHOOL.Models;
using BIGSCHOOL.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModels
            {
                Categories = dbContext.Categories.ToList(),
                Heading= "Add Course"
            };
            return View(viewModel);
        }

        // GET: Coursesggg
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModels viewModel)
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
            return RedirectToAction("Mine","Courses");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            var viewModel = new CoursesViewModels
            {
                UpComingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var courses = dbContext.Courses
                .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now  && c.IsCancled== false)
                .Include(l => l.Lecturer)
                .Include(c => c.Category)
                .ToList();
            return View(courses);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            var viewModel = new CourseViewModels
            {
                Categories = dbContext.Categories.ToList(),
                Date = course.DateTime.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryId,
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id
            };
            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModels viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = dbContext.Categories.ToList();
                return View("Create", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var course = dbContext.Courses.Single(c => c.Id == viewModel.Id && c.LecturerId == userId);

            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.Category;

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var courses = dbContext.Followings
                .Where(a => a.FollowerId  == userId)
                .Select(a => a.Followee)
                .ToList();

            var viewModel = new CoursesViewModels
            {
                UpComingFollowing = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
    }
}