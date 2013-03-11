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
            List<student> result;
            using (var client = new WebClient())
            {
                var json = client.DownloadString(url +"students.json");
                var serializer = new JavaScriptSerializer();
                result = serializer.Deserialize < List<student> > (json);
            }
            return View(result);
            //return View(db.students.ToList());
        }
        //
        // GET: /student/Details/5

        public ActionResult Details(int id = 0)
        {
            student student = new student();
            using (var client = new WebClient())
            {
                var json = client.DownloadString(url + "students/" + id.ToString() + ".json");
                var serializer = new JavaScriptSerializer();
                student = serializer.Deserialize<student>(json);
            }
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

        //create rails
        [HttpPost]
        public ActionResult Create(student student)
        {
            /*if (ModelState.IsValid)
            {
                var serializer = new JavaScriptSerializer();
                string a = serializer.Serialize(student);
                /*HttpWebRequest req = WebRequest.Create(new Uri(url))
                       as HttpWebRequest;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                Stream post = req.GetRequestStream();*/
              //  Server.Execute(url+"students/new.json");
                /*
                db.students.Add(student);
                db.SaveChanges();*/
                //return RedirectToAction("Index");
            //}
            var serializer = new JavaScriptSerializer();
            string a = serializer.Serialize(student);
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            req.Proxy = new System.Net.WebProxy(url+"students/new.json", true);
            //Add these, as we're doing a POST
            req.ContentType = "localhost:3000/students/new.json";//"application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(a);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            //return sr.ReadToEnd().Trim();

            return View(student);
        }
        //
        // POST: /student/Create

        /*[HttpPost]
        public ActionResult Create(student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }*/

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