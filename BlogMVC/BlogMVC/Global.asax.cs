using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlogMVC.Container;
using BlogMVC.Models.StrategyInitialization;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

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
        }
    }


}
