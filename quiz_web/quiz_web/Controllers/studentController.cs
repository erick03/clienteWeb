using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using quiz_web.Models;

namespace quiz_web.Controllers
{
    public class studentController : Controller
    {
        private CourseDBcontext db = new CourseDBcontext();
        private studentsDBContext dbStudent = new studentsDBContext();
        private string url = "http://localhost:3000/";
        //
        // GET: /student/
        public ActionResult Index()
        {
            return View(dbStudent.info());
            //return View(db.students.ToList());
        }

        [ActionName("studentCourse")]
        public ActionResult studentCourse(int id = 1)
        {
            List<Course> student_courses = dbStudent.get_courses(id);
            //Datos asociados a el curso
            ViewBag.studentsCourse = student_courses;
            return View();
        }
        private TestDBcontext dbT = new TestDBcontext();
        [ActionName("Quiz")]
        public ActionResult Quiz(int id = 1)
        {
            
            return View(dbT.info(id));
        }
        //
        // GET: /student/Details/5

        public ActionResult Details(int id = 0)
        {
            student student = new student();
            student = dbStudent.find(id);
            //student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /student/Create

        public ActionResult Create()
        {
            return View();
        }
        
        //
        // POST: /student/Create

        [HttpPost]
        public ActionResult Create(student student)
        {
            if (ModelState.IsValid)
            {
                dbStudent.create(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        //
        // GET: /student/Edit/5

        public ActionResult Edit(int id = 0)
        {
            student student = new student();
            student = dbStudent.find(id);
            //student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /student/Edit/5

        [HttpPost]
        public ActionResult Edit(student student)
        {
            dbStudent.edit(student);
            return View(student);
        }

        //
        // GET: /student/Delete/5

        public ActionResult Delete(int id = 0)
        {
            student student = new student();
            student = dbStudent.find(id);
            //student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        //
        // POST: /student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            student student = new student();
            student = dbStudent.find(id);
            //student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            dbStudent.delete(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            dbStudent.Dispose();
            base.Dispose(disposing);
        }
    }
}