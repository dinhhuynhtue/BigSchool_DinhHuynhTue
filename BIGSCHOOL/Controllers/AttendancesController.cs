using BIGSCHOOL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BIGSCHOOL.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext dbContext;

        public AttendancesController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int courseId)
        {
            var userId = User.Identity.GetUserId();
            if (dbContext.Attendances.Any(a => a.AttendanceID == userId && a.CourseId == courseId))
                return BadRequest("The Attendance already exists !");
            var attendance = new Attendance
            {
                CourseId = courseId,
                AttendanceID= userId
            };

            dbContext.Attendances.Add(attendance);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
