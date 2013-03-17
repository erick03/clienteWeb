using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace quiz_web.Models
{
    public class student
    {
        public int ID { get; set; }
        public string username {get ; set;}
        public string email { get; set; }
        public string identification { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class studentsDBContext : DbContext
    {
        private string url = "http://localhost:3000/students";
        //public student student = new student();
        private string data = ".json";

        public List<student> info()
        {
            var json = new Enlace().EjecutarAccion(url+".json", "GET");
            var serializer = new JavaScriptSerializer();
            List<student> result = serializer.Deserialize<List<student>>(json);
            return result;
        }
        public student find(int id)
        {
            var json = new Enlace().EjecutarAccion(url+"/"+id.ToString()+".json","GET");
            //client.DownloadString(url + "students/" + id.ToString() + ".json");
            var serializer = new JavaScriptSerializer();
            student result = serializer.Deserialize<student>(json);
            return result;
        }
        public student create(student student)
        {
            return new JavaScriptSerializer().Deserialize<student>(
            new Enlace().EjecutarAccion(url + ".json", "POST", student));
        }

        public student edit(student student)
        {
            return new JavaScriptSerializer().Deserialize<student>(
            new Enlace().EjecutarAccion(url +"/"+student.ID.ToString()+ data, "PUT", student));
        }

        public student delete(student student)
        {
            return new JavaScriptSerializer().Deserialize<student>(
            new Enlace().EjecutarAccion(url + "/" + student.ID.ToString() + data, "DELETE", student));
        }
        //public DbSet<student> students { get; set; }
    }
}