using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using quiz_web.Models;

namespace quiz_web.Controllers
{
    public class professorController : Controller
    {
        private professorsDBContext db = new professorsDBContext();
        //private string url = "http://localhost:3000/";
        //
        // GET: /professor/

        public ActionResult Index()
        {
            return View(db.info());
        }

        //
        // GET: /professor/Details/5
        [ActionName("professorCourse")]
        public ActionResult studentCourse(int id = 1)
        {
            List<Course> student_courses = db.get_courses(id);
            //Datos asociados a el curso
            ViewBag.Student = id;
            ViewBag.studentsCourse = student_courses;
            return View();
        }
        public ActionResult Details(int id = 0)
        {
            professors professor = new professors();
            professor = db.find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        //
        // GET: /professor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /professor/Create

        [HttpPost]
        public ActionResult Create(professors professors)
        {
            if (ModelState.IsValid)
            {
                db.create(professors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professors);
        }

        //
        // GET: /professor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            professors professors = new professors();
            professors = db.find(id);
            return View(professors);
        }

        //
        // POST: /professor/Edit/5

        [HttpPost]
        public ActionResult Edit(professors professors)
        {
            db.edit(professors);
            return View(professors);
        }

        //
        // GET: /professor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            professors professor = new professors();
            professor = db.find(id);
            return View(professor);
        }

        //
        // POST: /professor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            professors professors = db.find(id);
            db.delete(professors);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}