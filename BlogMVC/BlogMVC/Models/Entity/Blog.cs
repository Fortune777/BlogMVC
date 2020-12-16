using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMVC.Models.Entity
{
    public class Blog
    {
        public string BlogId { get; set; }

        public string Header { get; set; }

        public string Body { get; set; }

        public DateTime LastEditDateTime { get; set; }  
    }
}
