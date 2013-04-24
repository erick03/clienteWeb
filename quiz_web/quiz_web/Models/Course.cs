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
        public string description { get; set; }
    }
    public class log
    {
        public bool value { get; set; }
        public string role { get; set; }
        public string identification { get; set; }
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

        #region Metodos de los datos de cursos professor asociado y students asociados a el curso
        public List<student> studentAsociado(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/get_students.json?id=" + id.ToString(), "GET");
            var serializer = new JavaScriptSerializer();
            List<student> result = serializer.Deserialize<List<student>>(json);
            return result;
        }

        public List<student> notStudentAsociado(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/get_students_inverse.json?id=" + id.ToString(), "GET");
            var serializer = new JavaScriptSerializer();
            List<student> result = serializer.Deserialize<List<student>>(json);
            return result;
        }

        public Course detailCourse(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/" + id.ToString() + ".json", "GET");
            var serializer = new JavaScriptSerializer();
            Course result = serializer.Deserialize<Course>(json);
            return result;
        }

        public List<professors> professorCourse(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/get_professors.json?id=" + id.ToString(), "GET");
            var serializer = new JavaScriptSerializer();
            List<professors> result = serializer.Deserialize<List<professors>>(json);
            return result;
        }

        public List<professors> notProfessorInCourse(int id)
        {
            var json = new Enlace().EjecutarAccion(url + "/get_professors_inverse.json?id=" + id.ToString(), "GET");
            var serializer = new JavaScriptSerializer();
            List<professors> result = serializer.Deserialize<List<professors>>(json);
            return result;
        }

        public void DeleteStudentCourse(int idStudent, int idCourse)
        {
            new Enlace().EjecutarAccion("http://localhost:3000/course_students/delete_row.json?course_id=" + idCourse + "&student_id=" + idStudent, "GET");
        }

        public void DeleteProfessorCourse(int idProfessor, int idCourse)
        {
            new Enlace().EjecutarAccion("http://localhost:3000/course_professor/delete_row.json?course_id=" + idCourse + "&professor_id=" + idProfessor, "GET");
        }

        public void AddStudentCourse(datosAsocia a)
        {
            new Enlace().EjecutarAccion("http://localhost:3000/course_students.json", "POST", a);
            //var json = new Enlace().EjecutarAccion("http://localhost:3000/course_students.json?course_id="+idCourse+"&student_id="+idstudent, "POST");
            //return new JavaScriptSerializer().Deserialize<List<Course>>(json);
        }

        public void AddProfessorCourse(datosAsociaP p)
        {
            new Enlace().EjecutarAccion("http://localhost:3000/course_professors.json", "POST", p);
        }
        #endregion

        public log Login(LoginModel model, bool persistCookie = false)
        {
            string url2 = "http://localhost:3000/courses/login.json?";
            //bool result = false;
            //string url2 = "http://localhost:3000/courses/login.json";
            //bool result = false;
            string ver = model.UserName.ToString();
            var json = new Enlace().EjecutarAccion(url2 + "username=" + model.UserName.ToString() + "&password=" + model.Password.ToString(), "GET");
            var serializer = new JavaScriptSerializer();
            log result2 = serializer.Deserialize<log>(json);
            //if (result2.value)
            //{
                //ViewBag.log = log;
                //return RedirectToAction("Index", "Home");
            //    Course result = serializer.Deserialize<Course>(json);
            //    result = true;
            //}
            //else
            //    result = false;
            return result2;
        }

        private bool RedirectToAction(string p1, string p2)
        {
            throw new NotImplementedException();
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
