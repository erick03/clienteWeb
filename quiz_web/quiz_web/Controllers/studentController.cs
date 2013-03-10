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
using quiz_web.Models;

namespace quiz_web.Controllers
{
    public class studentController : Controller
    {
        private studentsDBContext db = new studentsDBContext();

        //
        // GET: /student/
        public ActionResult Index()
        {
            /*HttpWebRequest req = WebRequest.Create("http://localhost:3000/students.json")
                      as HttpWebRequest;*/
            student model;
            //var projects = new LinkedList<IEnumerable<>>;
            using (var client = new WebClient())
            {
                string json = client.DownloadString("http://localhost:3000/students.json");
                var serializer = new JavaScriptSerializer();
                model = serializer.Deserialize<student>(json);
            }
            /*
            using (HttpWebResponse resp = req.GetResponse()
                                          as HttpWebResponse)
            {
                StreamReader reader =
                    new StreamReader(resp.GetResponseStream());
                json = reader.ReadToEnd();
            }*/
            //var serializer = new JavaScriptSerializer();
            //model = serializer.Deserialize<student>(json);
            return View(model);//View(db.students.ToList());

            /*using (var client = new WebClient())
            {
                string json = client.DownloadString(url + "/" + pID.ToString() + Formato);
                var serializer = new JavaScriptSerializer();
                model = serializer.Deserialize<Professor>(json);
            }
            return model;*/
        }

        public student all()
        {
            student model;
            //var projects = new LinkedList<IEnumerable<>>;
            using (var client = new WebClient())
            {
                string json = client.DownloadString("http://localhost:3000/students.json");
                var serializer = new JavaScriptSerializer();
                model = serializer.Deserialize<student>(json);
            }
            return model;
        }

        //
        // GET: /student/Details/5

        public ActionResult Details(int id = 0)
        {
            student student = db.students.Find(id);
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
                db.students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        //
        // GET: /student/Edit/5

        public ActionResult Edit(int id = 0)
        {
            student student = db.students.Find(id);
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
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //
        // GET: /student/Delete/5

        public ActionResult Delete(int id = 0)
        {
            student student = db.students.Find(id);
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
            student student = db.students.Find(id);
            db.students.Remove(student);
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