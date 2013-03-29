using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace quiz_web.Models
{
    public class Course 
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string descripction { get; set; }
    }
    public class log
    {
        public bool value { get; set; }
    }
    public class CourseDBcontext : DbContext
    {
        //public DbSet<Course> Course { get; set; }
        private string url = "http://localhost:3000/courses";
        string data = ".json";
        public List<Course> info()
        {
            var json = new Enlace().EjecutarAccion(url + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            List<Course> result = serializer.Deserialize<List<Course>>(json);
            return result;
        }
        public bool Login(LoginModel model, bool persistCookie = false)
        {
            string url2 = "http://localhost:3000/courses/login";
            bool result = false;
            string ver = model.UserName.ToString();
            var json = new Enlace().EjecutarAccion(url2 +".json", "GET");
            var serializer = new JavaScriptSerializer();
            //string estado = json.ToString().Substring(19,23);
            log result2 = serializer.Deserialize<log>(json);
            if (result2.value)
            {
                result = true;
            }
            else
                result = false;
            return result;
        }

        public Course find(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/" + id.ToString() + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            Course result = serializer.Deserialize<Course>(json);
            return result;
        }
        //
        public Course create(Course courses)
        {
            return new JavaScriptSerializer().Deserialize<Course>(
            new Enlace().EjecutarAccion(url + ".json", "POST", courses));
        }

        public Course edit(Course course)
        {
            return new JavaScriptSerializer().Deserialize<Course>(
            new Enlace().EjecutarAccion(url + "/" + course.ID.ToString() + data, "PUT", course));
        }

        public Course delete(Course course)
        {
            return new JavaScriptSerializer().Deserialize<Course>(
            new Enlace().EjecutarAccion(url + "/" + course.ID.ToString() + data, "DELETE", course));
        }
    }
}
