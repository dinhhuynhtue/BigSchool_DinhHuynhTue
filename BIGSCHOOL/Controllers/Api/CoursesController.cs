using BIGSCHOOL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BIGSCHOOL.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext dbContext;

        public CoursesController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            if (course.IsCancled)
                return NotFound();
            course.IsCancled = true;
            dbContext.SaveChanges();

            return Ok();
       
        }
    }
}
