using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMVC.Models.Entity
{
    public class Blog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        
        public string Title { get; set; }
        public string Body { get; set; }
        public  string UserId { get; set; }

        public DateTime LastEditDateTime { get; set; }  
    }
}
