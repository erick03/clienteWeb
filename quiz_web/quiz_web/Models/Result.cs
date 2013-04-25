using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace quiz_web.Models
{
    public class Result
    {
        public int ID { get; set; }
        public string grade { get; set; }
        public string note { get; set; }
        public int test_id { get; set; }
        public int student_id { get; set; }
    }

    public class ResultDBcontext : DbContext
    {
        //
        // GET: /Result/

        public List<Result> questionsAnswers(int id)
        {
            //1
            var json = new Enlace().EjecutarAccion("http://localhost:3000/tests/get_answers.json?test_id=" + id, "GET");
            var serializer = new JavaScriptSerializer();
            List<Result> result = serializer.Deserialize<List<Result>>(json);
            return result;
        }

        public Result create(Result result)
        {
            return new JavaScriptSerializer().Deserialize<Result>(
            new Enlace().EjecutarAccion("http://localhost:3000/results.json", "POST", result));
        }

        public DbSet<Result> Results { get; set; }

    }
}
