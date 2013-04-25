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
    public class TestController : Controller
    {
        private TestDBcontext db = new TestDBcontext();
        private QuestionsDBcontext dbQuestion = new QuestionsDBcontext();
        //int variable = 0;
        //
        // GET: /Test/

        public ActionResult Index(int idCourse = 0 , int idStudent = 0)
        {
            ViewBag.Student = idStudent; 
            ViewBag.Idcourse = idCourse;
            return View(db.info(idCourse));
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id = 0)
        {
            Test test = db.find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        //
        // GET: /Test/Create

        public ActionResult Create(int idCourse)
        {
            ViewBag.Courseid = idCourse;
            Test test = new Test();
            test.course_id = idCourse;
            return View();
        }

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(Test test)
        {
            string ptt = "";
            ptt = RouteData.Values["idCourse"] + Request.Url.Query;
            ptt = ptt.Replace("?idCourse=", ""); //idCourse=1
            test.course_id = int.Parse(ptt);
            if (ModelState.IsValid)
            {
                db.create(test);
                db.SaveChanges();
                return RedirectToAction("Index", new { idCourse = test.course_id });
            }
            return RedirectToAction("Index", new { idCourse = test.course_id });
        }

        //
        // GET: /Test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Test test = db.find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        public ActionResult Edit(Test test)
        {
            db.edit(test);
            return View(test);
        }

        //
        // GET: /Test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Test test = db.find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = db.find(id);
            db.delete(test);
            db.SaveChanges();
            return RedirectToAction("Index", new { idCourse = test.course_id });
        }

        //Carga las preguntas del quiz
        [ActionName("Answers")]
        public ActionResult Answers(int id = 0, int idStudent = 0)
        {
            if (idStudent == 0)
            {
                return HttpNotFound();
            }
            ViewBag.testID = id;
            //var n = (((quiz_web.Models.log)(System.Web.HttpContext.Current.Session["usuario"])).identification);
            ViewBag.Student = idStudent;
            //ViewBag.courseId = idCourse;
            ViewBag.Questions = dbQuestion.questionsTest(id);
            return View();
        }
        /*


        [ActionName("Qualify")]
        public ActionResult Qualify(int id = 0)
        {
            ViewBag.testID = id;
            //ViewBag.courseId = idCourse;
            ViewBag.Questions = dbQuestion.questionsTest(id);
            ViewBag.Answers = dbQuestion.answersTest(id, 0);
            return View();
        }
        */
        [ActionName("StudentsThatAnswer")]
        public ActionResult StudentsThatAnswer(int id = 0)
        {
            ViewBag.testID = id;
            ViewBag.Student = dbQuestion.StudentsThatAnswer(id);
            return View();
        }

        [ActionName("AnswersNew")]
        public ActionResult AnswersNew(Answer answer, int idTest = 0, int idStudent = 0, int idQues = 0)
        {
            ViewBag.testID = idTest;
            answer.coso = "";
            answer.test_id = idTest;
            answer.student_id = idStudent;
            answer.question_id = idQues;
           
            //ViewBag.courseId = idCourse;
            //ViewBag.Questions = dbQuestion.questionsTest(id);
            return View();
        }

        [HttpPost]
        public ActionResult Response()
        {
            if (ModelState.IsValid)
            {
                string answer = Request["coso"];
                int idPregunta = Convert.ToInt32(Request["question_id"]);
                int student_id = Convert.ToInt32(Request["student_id"]);
                int test_id = Convert.ToInt32(Request["test_id"]);

                Answer ResultAnswer = new Answer();
                ResultAnswer.question_id = idPregunta;
                ResultAnswer.student_id = student_id;
                ResultAnswer.test_id = test_id;
                ResultAnswer.coso = answer;
                var pregunta = db.AnswersNew(ResultAnswer);
                return RedirectToAction("Answers", "Answers", new { id = test_id, idStudent = student_id });
                //return RedirectToAction("IndexEstudiante", new { id = test_id, idEst = student_id });
            }
            return RedirectToAction("Index");
            //return RedirectToAction("Answers", "Answers", new { id = test_id, idStudent = student_id });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}