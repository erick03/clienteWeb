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
    public class QuestionsController : Controller
    {
        private QuestionsDBcontext db = new QuestionsDBcontext();

        //
        // GET: /Questions/

        public ActionResult Index(int id = 0)
        {
            ViewBag.testID = id;
            return View(db.questionsTest(id));
        }

        [ActionName("questionsTest")]
        public ActionResult questionsTest(int id = 0)
        {
            List<Questions> questionsTest = db.questionsTest(id);
            if (questionsTest == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionsTest = questionsTest;
            return View();
        }
        //
        // GET: /Questions/Details/5

        public ActionResult Details(int id = 0)
        {
            Questions questions = db.find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        //
        // GET: /Questions/Create

        public ActionResult Create(int testid = 0)
        {
            ViewBag.idTest = testid;
            Questions question = new Questions();
            question.test_id = testid;
            return View();
        }

        //
        // POST: /Questions/Create

        [HttpPost]
        public ActionResult Create(Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.create(questions);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = questions.test_id });
            }

            return View(questions);
        }

        //
        // GET: /Questions/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Questions questions = db.find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        public ActionResult Edit(Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questions);
        }

        //
        // GET: /Questions/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Questions questions = db.Test.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions questions = db.find(id);
            db.delete(questions);
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