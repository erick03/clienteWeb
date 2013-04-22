using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace quiz_web.Models
{
    public class Test
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public bool status { get; set; }
        public string duration { get; set; }
    }

    public class TestDBcontext : DbContext
    {
        public DbSet<Test> Test { get; set; }
        private string url = "http://localhost:3000/tests";
        //public student student = new student();
        private string data = ".json";

        public List<Test> info()
        {
            var json = new Enlace().EjecutarAccion(url + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            List<Test> result = serializer.Deserialize<List<Test>>(json);
            return result;
        }

        public Test find(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/" + id.ToString() + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            Test result = serializer.Deserialize<Test>(json);
            return result;
        }

        public Test create(Test test)
        {
            return new JavaScriptSerializer().Deserialize<Test>(
            new Enlace().EjecutarAccion(url + ".json", "POST", test));
        }

        public Test edit(Test test)
        {
            return new JavaScriptSerializer().Deserialize<Test>(
            new Enlace().EjecutarAccion(url + "/" + test.ID.ToString() + data, "PUT", Test));
        }

        public Test delete(Test test)
        {
            return new JavaScriptSerializer().Deserialize<Test>(
            new Enlace().EjecutarAccion(url + "/" + test.ID.ToString() + data, "DELETE", test));
        }
    }
}