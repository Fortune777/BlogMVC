using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMVC.Models.Entity
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public IdentityUser User { get; set; } 
        public DateTime LastEditDateTime { get; set; }  
    }
}
