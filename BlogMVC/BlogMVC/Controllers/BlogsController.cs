using System;
using BlogMVC.Models;
using BlogMVC.Models.Entity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMVC.Controllers
{

    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogsController(ApplicationDbContext dbContex)
        {
            _dbContext = dbContex;
        }

        // GET: Blogs
        public async Task<ActionResult> Index()
        {
           // найти где лежит юзер Id
            // var claims = HttpContext.User.Identity.i
            var userId = (HttpContext.User.Identity as ClaimsIdentity).FindFirstValue("id");

            var blogs = await _dbContext.BLogs.AsNoTracking().Where(x => x.BlogId == userId).ToArrayAsync();

            return View(blogs);
        }

        // GET: Blogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await _dbContext.BLogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Header,Body")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.BlogId = (HttpContext.User.Identity as ClaimsIdentity).FindFirstValue("id");
                blog.LastEditDateTime = DateTime.UtcNow;
                _dbContext.BLogs.Add(blog);

                try
                { 
                    _dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await _dbContext.BLogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Header,Body")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(blog).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await _dbContext.BLogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Blog blog = await _dbContext.BLogs.FindAsync(id);
            _dbContext.BLogs.Remove(blog);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
