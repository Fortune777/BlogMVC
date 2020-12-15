using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogMVC.Models.DTO
{
    public class BlogDto
    {
        [HiddenInput]
        public  int BlogId { get; set; }
        [Required]
        public string Header { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}
