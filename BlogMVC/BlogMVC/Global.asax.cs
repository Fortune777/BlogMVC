using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlogMVC.Container;
using BlogMVC.Models.StrategyInitialization;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Serilog;
using Serilog.Core;

namespace BlogMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Database.SetInitializer(new InitializationDb());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // внедрение зависимостей
            NinjectModule registrations = new LogicDIModule();
            var kernel = new StandardKernel(registrations);

            // для исправления ошибки на стороне клиента 
            // https://stackoverflow.com/questions/62751932/getting-the-following-error-in-asp-net-mvc-5-validation-type-names-in-unobtrusi
            kernel.Unbind<ModelValidatorProvider>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));


            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\log.txt";
            var log = new LoggerConfiguration()
                .WriteTo.File(path,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception} {Properties:j}",
                    encoding: Encoding.Default)
                .Enrich.WithMvcControllerName()
                .Enrich.WithMvcActionName()
                .Enrich.WithMvcRouteData()
                .Enrich.WithMvcRouteTemplate()
                .CreateLogger();

            // Debug.Write(path);

            kernel.Bind<ILogger>().ToConstant(log);

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Log.CloseAndFlush();
        }
    }


}
