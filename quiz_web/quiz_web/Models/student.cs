using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace quiz_web.Models
{
    public class student
    {
        public int ID { get; set; }
        public string username {get ; set;}
        public string email { get; set; }
        public string identification { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        /*public string created_at { get; set; }
        public string updated_at { get; set; }*/
    }

    public class studentsDBContext : DbContext
    {
        public DbSet<student> students { get; set; }
    }
}