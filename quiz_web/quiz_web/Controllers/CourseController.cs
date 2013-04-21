using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quiz_web.Models;

namespace quiz_web.Controllers
{
    public class CourseController : Controller
    {
        private CourseDBcontext db = new CourseDBcontext();

        //
        // GET: /Course/

        public ActionResult Index()
        {
            return View(db.info());
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id = 0)
        {
            Course course = db.find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.create(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Course course = db.find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            db.edit(course);
            return View(course);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Course course = db.find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [ActionName("Asociaciones")]
        public ActionResult asociaciones()
        {
            return View();
        }
        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.find(id);
            db.delete(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /*[ActionName("asignarestudiante")]
        public ActionResult Assign(int id = 0)
        {
            Estudiante[] estudiantescurso = db.obtenerEstudiantes(id);
            if (estudiantescurso == null)
            {
                return HttpNotFound();
            }

            Estudiante[] estudiantesnocurso = db.obtenerNoEstudiantes(id);

            ViewBag.noestudiantes = estudiantesnocurso;
            ViewBag.profesor = db.obtenerProfesor(id);
            ViewBag.noprofesor = db.obtenerNoProfesores(id);

            var curso = db.ObtenerDetalle(id);
            ViewBag.curso = curso;

            return View(estudiantescurso);
        }*/
    }
}