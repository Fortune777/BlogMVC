using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Services;
using AutoMapper;
using BlogMVC.Models;
using Microsoft.Owin.Logging;
using Ninject;
using Ninject.Modules;
using AutoMapper.Mappers;
using BlogMVC.Models.DTO;
using BlogMVC.Models.Entity;
using BlogMVC.Models.ProfileAutomapper;

namespace BlogMVC.Container
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
             Mapper.Initialize(cfg => cfg.AddProfiles(typeof(BlogProfile)));
             var mapper = Mapper.Configuration.CreateMapper();
             this.Bind<IMapper>().ToConstant(mapper);

            this.Bind<ApplicationDbContext>().ToSelf();
         
        }
    }
}
