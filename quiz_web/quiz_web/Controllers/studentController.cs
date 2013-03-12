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
        private studentsDBContext db = new studentsDBContext();
        private string url = "http://localhost:3000/";
        //
        // GET: /student/
        public ActionResult Index()
        {
            return View(db.info());
            //return View(db.students.ToList());
        }
        //
        // GET: /student/Details/5

        public ActionResult Details(int id = 0)
        {
            student student = new student();
            student = db.find(id);
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
                db.create(student);
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
            using (var client = new WebClient())
            {
                //student = db.students.Find(id);
                var json = client.DownloadString(url + "students/"+id.ToString()+".json");
                var serializer = new JavaScriptSerializer();
                student = serializer.Deserialize<student>(json);
                if (student == null)
                {
                    return HttpNotFound();
                }
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
            student student = new student();
            using (var client = new WebClient())
            {
                //student = db.students.Find(id);
                var json = client.DownloadString(url + "students/" + id.ToString() + ".json");
                var serializer = new JavaScriptSerializer();
                student = serializer.Deserialize<student>(json);
                if (student == null)
                {
                    return HttpNotFound();
                }
            }
            return View(student);
        }
        //Borrar para rails
        public ActionResult DeleteConfirmedR(int id)
        {
            WebRequest request = WebRequest.Create(url + "students/" + id.ToString() + ".json");
            request.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return RedirectToAction("Index");
        }
        //
        // POST: /student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            /*student student = db.students.Find(id);
            db.students.Remove(student);
            db.SaveChanges();*/
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}