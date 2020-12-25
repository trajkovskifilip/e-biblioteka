using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_biblioteka.Models;
using E_biblioteka.Models.Forum;
using Microsoft.AspNet.Identity;

namespace E_biblioteka.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private IApplicationDbContext db;
        private ApplicationUser au = new ApplicationUser();

        public PostsController()
        {
            db = new ApplicationDbContext();
        }

        public PostsController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        // GET: AddCommentToPost
        //

        // GET: Posts
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View(db.Query<Post>().ToList());
        }

        // GET: Posts/Create
        public ActionResult Create(int? BookId)
        {
            List<Book> Books = new List<Book>();

            if (BookId != null)
            {
                Books.Add(db.Query<Book>().FirstOrDefault(b => b.BookId == BookId));
            }
            else
            {
                Books = db.Query<Book>().ToList();
            }
            NewPost model = new NewPost();
            {
                model.Books = Books;
                var user = db.Query<ApplicationUser>().FirstOrDefault(u => u.Id == User.Identity.GetUserId());
                if (user != null)
                {
                    model.Title = user.UserName;
                }
            }
            return View(model);
        }


        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,BookId,Title,Content")] NewPost model)
        {
            Post post = new Post();
            if (ModelState.IsValid)
            {
                try
                {
                    post.UserId = User.Identity.GetUserId();
                    post.Title = model.Title;
                    post.BookId = model.BookId;
                    post.Content = model.Content;
                    post.SelectedBook = db.Query<Book>().FirstOrDefault(b => b.BookId == model.BookId);
                    post.User = db.Query<ApplicationUser>().FirstOrDefault(u => u.Id == User.Identity.GetUserId());
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.Add(post);
                db.SaveChanges();
                Book book = db.Query<Book>().FirstOrDefault(b => b.BookId == model.BookId);
                return RedirectToAction("Details", "Books", new { id = book.BookId, page = 1});
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int? id)
        {
            string userId = User.Identity.GetUserId();
            if (id != null && !IsAuthorized(id.Value, userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Query<Post>().FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit([Bind(Include = "Id,UserId,BookId,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                if (!IsAuthorized(post.Id, userId))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
                Post ChangePost = db.Query<Post>().FirstOrDefault(p => p.Id == post.Id);
                Book SelectedBook = db.Query<Book>().FirstOrDefault(b => b.BookId == post.BookId);
                //ApplicationUser User = db.Query<ApplicationUser>().FirstOrDefault(u => u.Id == post.UserId);
                ChangePost.Title = post.Title;
                ChangePost.Content = post.Content;
                //ChangePost.SelectedBook = post.SelectedBook = SelectedBook;
                //ChangePost.User = User;
                //ChangePost.UserId = post.UserId;
                //ChangePost.BookId = post.BookId;

                db.SaveChanges();
                return RedirectToAction("Details", "Books", new { id = post.BookId, page = 1 });
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int? id)
        {
            string userId = User.Identity.GetUserId();
            if (id != null && !IsAuthorized(id.Value, userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Query<Post>().FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            string userId = User.Identity.GetUserId();
            if (!IsAuthorized(id, userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            Post post = db.Query<Post>().FirstOrDefault(p => p.Id == id);
            db.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Details", "Books", new { id = post.BookId , page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsAuthorized(int id, string userId)
        {
            int PostId = id;
            Post post = db.Query<Post>().FirstOrDefault(p => p.Id == PostId);
            ApplicationUser user = db.Query<ApplicationUser>().FirstOrDefault(u => u.Id == userId);
            string RoleId = GetUserRole(userId);
            if (RoleId == "1" || RoleId == "3" || post.UserId == userId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GetUserRole(string UserId)
        {
            ApplicationUser user = db.Query<ApplicationUser>().FirstOrDefault(u => u.Id == UserId);
            try
            {
                string roleId = user.Roles.ToList().FirstOrDefault(m => m.UserId == UserId).RoleId; // 1, 3
                return roleId;
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
