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
        private string url = "http://localhost:3000/";
        //
        // GET: /professor/

        public ActionResult Index()
        {
            List<professors> result;
            using (var client = new WebClient())
            {
                var json = client.DownloadString(url + "professors.json");
                var serializer = new JavaScriptSerializer();
                result = serializer.Deserialize<List<professors>>(json);
            }
            return View(result);
            //return View(db.professor.ToList());
        }

        //
        // GET: /professor/Details/5

        public ActionResult Details(int id = 0)
        {
            professors professor = new professors();
            using (var client = new WebClient())
            {
                var json = client.DownloadString(url + "professors/" + id.ToString() + ".json");
                var serializer = new JavaScriptSerializer();
                professor = serializer.Deserialize<professors>(json);
            }
            //student student = db.students.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
            /*professors professors = db.professor.Find(id);
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);*/
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
                db.professor.Add(professors);
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
            using (var client = new WebClient())
            {
                //student = db.students.Find(id);
                var json = client.DownloadString(url + "professors/" + id.ToString() + ".json");
                var serializer = new JavaScriptSerializer();
                professors = serializer.Deserialize<professors>(json);
                if (professors == null)
                {
                    return HttpNotFound();
                }
            }
            return View(professors);
            /*
            professors professors = db.professor.Find(id);
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);*/
        }

        //
        // POST: /professor/Edit/5

        [HttpPost]
        public ActionResult Edit(professors professors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professors);
        }

        //
        // GET: /professor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            professors professor = new professors();
            using (var client = new WebClient())
            {
                //student = db.students.Find(id);
                var json = client.DownloadString(url + "professors/" + id.ToString() + ".json");
                var serializer = new JavaScriptSerializer();
                professor = serializer.Deserialize<professors>(json);
                if (professor == null)
                {
                    return HttpNotFound();
                }
            }
            return View(professor);
            /*
            professors professors = db.professor.Find(id);
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);*/
        }

        //
        // POST: /professor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            professors professors = db.professor.Find(id);
            db.professor.Remove(professors);
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