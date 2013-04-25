using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace quiz_web.Models
{
    public class professors
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string identification { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class professorsDBContext : DbContext
    {
        //public DbSet<professors> professor { get; set; } 
        private string url = "http://localhost:3000/professors";
        string data = ".json";
        public List<professors> info()
        {
            var json = new Enlace().EjecutarAccion(url + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            List<professors> result = serializer.Deserialize<List<professors>>(json);
            return result;
        }
        public List<Course> get_courses(int id)
        {
            var json = new Enlace().EjecutarAccion("http://localhost:3000/courses/get_courses_professor.json?id=" + id, "GET");
            var serializer = new JavaScriptSerializer();
            List<Course> result = serializer.Deserialize<List<Course>>(json);
            return result;
        }
        //
        public professors find(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/" + id.ToString() + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            professors result = serializer.Deserialize<professors>(json);
            return result;
        }
        //
        public professors create(professors professor)
        {
            return new JavaScriptSerializer().Deserialize<professors>(
            new Enlace().EjecutarAccion(url + ".json", "POST", professor));
        }
        //
        public professors edit(professors professor)
        {
            return new JavaScriptSerializer().Deserialize<professors>(
            new Enlace().EjecutarAccion(url + "/" + professor.ID.ToString() + data, "PUT", professor));
        }

        public professors delete(professors professor)
        {
            return new JavaScriptSerializer().Deserialize<professors>(
            new Enlace().EjecutarAccion(url + "/" + professor.ID.ToString() + data, "DELETE", professor));
        }
    }
}