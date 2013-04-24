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
        public string coso { get; set; }
        public int test_id { get; set; }
    }

    public class Answers
    {
        public int ID { get; set; }
        public string answer { get; set; }
        public int test_id { get; set; }
    }

    public class Student
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string identification { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
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

        public List<Answers> answersTest(int id, int student_id)
        {
            //1
            var json = new Enlace().EjecutarAccion("http://localhost:3000/tests/get_answers.json?test_id=" + id + "&student_id=" + student_id, "GET");
            var serializer = new JavaScriptSerializer();
            List<Answers> result = serializer.Deserialize<List<Answers>>(json);
            return result;
        }

        public List<Student> StudentsThatAnswer(int id)
        {
            //1
            var json = new Enlace().EjecutarAccion("http://localhost:3000/tests/students_that_answer.json?test_id=" + id, "GET");
            var serializer = new JavaScriptSerializer();
            List<Student> result = serializer.Deserialize<List<Student>>(json);
            return result;
        }

        public Questions find(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/" + id.ToString() + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            Questions result = serializer.Deserialize<Questions>(json);
            return result;
        }

        public Questions create(Questions question)
        {
            return new JavaScriptSerializer().Deserialize<Questions>(
            new Enlace().EjecutarAccion(url + ".json", "POST", question));
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