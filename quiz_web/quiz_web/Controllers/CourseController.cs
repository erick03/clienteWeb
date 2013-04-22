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
        private studentsDBContext dbStudent = new studentsDBContext();
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

        #region DATOS DE LOS COURSES CON SUS ALUMNOS Y PROFESSOR
        [ActionName("Asociaciones")]
        public ActionResult asociaciones(int id = 0)
        {
            List<student> studentCourse = db.studentAsociado(id);
            if (studentCourse == null)
            {
                return HttpNotFound();
            }
            List<professors> professorCourse = db.professorCourse(id);
            if (professorCourse == null)
            {
                return HttpNotFound();
            }
            Course course = db.find(id);

            List<student> notInCourseStudentCourse = db.notStudentAsociado(id);
            //Datos asociados a el curso
            ViewBag.professorsCourse = professorCourse;
            ViewBag.studentsCourse = studentCourse;
            ViewBag.infoCourse = course;

            //Datos no asociados a el curso
            ViewBag.notStudentCourse = notInCourseStudentCourse;
            ViewBag.notProfessors = null;
            
            return View();
        }

        [ActionName("DeleteStudentCourse")]
        public ActionResult DeleteStudentCourse(int idStudent, int idCourse)
        {
            db.DeleteStudentCourse(idStudent, idCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ActionName("AddStudentCourse")]
        public ActionResult AddStudentCourse(int idStudent , int idCourse)
        {
            datosAsocia a = new datosAsocia();
            a.course_id = idCourse;
            a.student_id = idStudent;
            db.AddStudentCourse(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        
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