using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace quiz_web.Models
{
    public class Questions
    {
        public int ID { get; set; }
        public string question { get; set; }
        public int test_id { get; set; }
    }

    public class QuestionsDBcontext : DbContext
    {
        public DbSet<Questions> Test { get; set; }
        private string url = "http://localhost:3000/questions";
        //public student student = new student();
        private string data = ".json";

        public List<Questions> info()
        {
            var json = new Enlace().EjecutarAccion(url + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            List<Questions> result = serializer.Deserialize<List<Questions>>(json);
            return result;
        }

        public List<Questions> questionsTest(int id)
        {
            //1
            var json = new Enlace().EjecutarAccion("http://localhost:3000/tests/get_questions.json?test_id="+id, "GET");
            var serializer = new JavaScriptSerializer();
            List<Questions> result = serializer.Deserialize<List<Questions>>(json);
            return result;
        }

        public Questions find(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/" + id.ToString() + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            Questions result = serializer.Deserialize<Questions>(json);
            return result;
        }

        public Questions create(Questions questions)
        {
            return new JavaScriptSerializer().Deserialize<Questions>(
            new Enlace().EjecutarAccion(url + ".json", "POST", questions));
        }

        public Questions edit(Questions questions)
        {
            return new JavaScriptSerializer().Deserialize<Questions>(
            new Enlace().EjecutarAccion(url + "/" + questions.ID.ToString() + data, "PUT", Test));
        }

        public Questions delete(Questions questions)
        {
            return new JavaScriptSerializer().Deserialize<Questions>(
            new Enlace().EjecutarAccion(url + "/" + questions.ID.ToString() + data, "DELETE", questions));
        }
    }
}