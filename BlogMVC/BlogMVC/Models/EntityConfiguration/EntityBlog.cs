using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogMVC.Models.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMVC.Models.EntityConfiguration
{
    public class EntityBlog : EntityTypeConfiguration<Blog>
    {
        public EntityBlog()
        {
            Property(p => p.Title).IsRequired().HasMaxLength(100);
            Property(p => p.Body).IsRequired().HasMaxLength(3000);
            Property(p => p.LastEditDateTime).IsRequired();
        }
    }
}
