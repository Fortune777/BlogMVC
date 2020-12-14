using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Services;
using BlogMVC.Models;
using Microsoft.Owin.Logging;
using Ninject;
using Ninject.Modules;

namespace BlogMVC.Container
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ApplicationDbContext>().ToSelf();
        }
    }
}
