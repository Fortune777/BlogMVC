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
        public  string BlogId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(3000)]
        public string Body { get; set; }
    }
}
