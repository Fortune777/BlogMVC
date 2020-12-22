using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMVC.Models;
using Serilog;

namespace BlogMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public HomeController(ApplicationDbContext dbContext,ILogger logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            _logger.Information("View Home/Index page" + this.User.Identity.Name);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}