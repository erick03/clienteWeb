using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace quiz_web.Models
{
    public class professors
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string identification { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
    }

    public class professorsDBContext : DbContext
    {
        //public DbSet<student> students { get; set; }
        public DbSet<professors> professor { get; set; } 
    }
}