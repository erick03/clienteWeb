using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace quiz_web.Models
{
    public class Test
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public bool status { get; set; }
        public string duration { get; set; }
    }

    public class TestDBcontext : DbContext
    {
        public DbSet<Test> Test { get; set; }
    }
}