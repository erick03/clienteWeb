using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quiz_web.Models
{
    public class Answer
    {
        public string coso { get; set; }
        public int question_id { get; set; }
        public int student_id { get; set; }
        public int test_id { get; set; }
    }
}