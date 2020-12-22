using System;
using BlogMVC.Models;
using BlogMVC.Models.Entity;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BlogMVC.Models.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Ast.Selectors;
using Serilog;

namespace BlogMVC.Controllers
{

    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BlogsController(ApplicationDbContext dbContex, IMapper mapper, ILogger logger)
        {
            _logger = logger;
            _dbContext = dbContex;
            _mapper = mapper;
        }


        // GET: Blogs
        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }


        [ChildActionOnly]
        public ActionResult RenderBlogList()
        {
            _logger.Information("getting by id all posts the user");
            var userId = (HttpContext.User.Identity as ClaimsIdentity).FindFirstValue("id");
            var blogs = _dbContext.BLogs.AsNoTracking()
                .Where(x => x.UserId == userId)
                .ProjectToList<BlogDto>();
            
            return PartialView("_PartialBlogList", blogs);
        }


        // GET: Blogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = await _dbContext.BLogs.FindAsync(id);
            var dto = _mapper.Map<BlogDto>(blog);

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Body")] BlogDto blog)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Blog>(blog);
                entity.UserId = (HttpContext.User.Identity as ClaimsIdentity).FindFirstValue("id");
                entity.LastEditDateTime = DateTime.UtcNow;
                
                _dbContext.BLogs.Add(entity);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e);
                }

                return RedirectToAction("Index");
            }


            return View();
        }

        // GET: Blogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Blog blog = await _dbContext.BLogs.FindAsync(id);
            var dto = _mapper.Map<BlogDto>(blog);

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Title,Body")] BlogDto blog)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(blog).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var dto = _mapper.Map<BlogDto>(blog);

            return View(dto);
        }

        // GET: Blogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Blog blog = await _dbContext.BLogs.FindAsync(id);
            var dto = _mapper.Map<BlogDto>(blog);

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(dto);
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
