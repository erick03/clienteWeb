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
    public class ResultController : Controller
    {
        private ResultDBcontext db = new ResultDBcontext();
        private QuestionsDBcontext dbQuestion = new QuestionsDBcontext();

        //
        // GET: /Result/


        public ActionResult Index()
        {
            return View(db.Results.ToList());
        }

        //
        // GET: /Result/Details/5

        public ActionResult Details(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // GET: /Result/Create
        [ActionName("questionsAnswers")]
        public ActionResult Create(int student_id = 0, int test_id = 0)
        {
            ViewBag.student_id = student_id;
            ViewBag.test_id = test_id;
            ViewBag.Questions = dbQuestion.questionsTest(test_id);
            ViewBag.Answers = dbQuestion.answersTest(test_id, student_id);
            return View();
        }

        //
        // POST: /Result/Create

        [HttpPost]
        public ActionResult Create(Result result)
        {
            if (ModelState.IsValid)
            {
                db.create(result);
                db.SaveChanges();
                return RedirectToAction("StudentsThatAnswer","TestController", new { idCourse = result.test_id });
            }
            return RedirectToAction("StudentsThatAnswer", "TestController", new { idCourse = result.test_id });
        }

        //
        // GET: /Result/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // POST: /Result/Edit/5

        [HttpPost]
        public ActionResult Edit(Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(result);
        }

        //
        // GET: /Result/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // POST: /Result/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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