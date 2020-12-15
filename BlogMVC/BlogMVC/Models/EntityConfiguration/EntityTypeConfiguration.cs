using System;
using System.Collections.Generic;
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
            ToTable("Blog").HasKey(p => p.BlogId);
            Property(p => p.Header).IsRequired().HasMaxLength(100);
            Property(p => p.Body).IsRequired().HasMaxLength(3000);
            Property(p => p.LastEditDateTime).IsRequired();
        }
    }
}
