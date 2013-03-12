using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quiz_web.Models
{
    public class Course 
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string descripction { get; set; }
    }

    public class CourseDBcontext : DbContext
    {
        public DbSet<Course> Course { get; set; } 
    }
}
