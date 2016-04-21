using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPSBlog.Models;

namespace MyPSBlog.Controllers
{
    /// CTRL + Shift + T
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Post.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin")] //This allows only the Admin person to login.
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,MediaUrl,Publish")] Blog post)
        {
            if (!ModelState.IsValid) return View(post);
            post.AuthorId = this.User.Identity.GetUserId();
            db.Post.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaUrl,Publish")] Blog post)
        {
            //post.UpdatedOn = new DateTimeOffset(DateTime.Now);//Added current date/time
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin1")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DeleteBlogViewModel
            {
                BlogId = post.Id
            };

            return View(viewModel);
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Blog post = db.Post.Find(model.BlogId);

            if (this.User.IsInRole("Admin") || this.User.Identity.GetUserId() == post.AuthorId)
            {
                db.Post.Remove(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Get lost");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class DeleteBlogViewModel
    {
        [Required]
        public int BlogId { get; set; }

        public string AuthorId { get; set; }
    }
}