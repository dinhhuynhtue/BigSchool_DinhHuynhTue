using BIGSCHOOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIGSCHOOL.Models.ViewModels
{

    public class CoursesViewModels
    {
        public IEnumerable<Course> UpComingCourses { get; set; }
        public bool ShowAction { get; set; }
    }
}