using Data;
using FirstProject.Business;
using FirstProject.ViewModels;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FirstProject.Controllers
{
    public class UsersController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginBusiness loginRestriction = new LoginBusiness();

        // GET: Users
        public ActionResult Index()
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return View(db.Users.ToList());
                }
                catch (Exception ex)
                {
                    return null;
                };
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Username,Password,IsActive,LastLoginDate")] User user)
        {
                db.Users.Add(user);
                db.SaveChanges();
         
                return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,IsActive,LastLoginDate")] User user)
        {
            if (loginRestriction.IsRestricted((int)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //GET:Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST:Login
        [HttpPost]
        public ActionResult Login([Bind(Include ="Username,Password")] LoginViewModel userViewModel)
        {
            try
            {
                Agency agency = db.Agencies.Include(x => x.User).Where(x => x.User.Username == userViewModel.Username && x.User.Password == userViewModel.Password).FirstOrDefault();
                if(agency != null)
                {
                    Session["agencyId"] = agency.Id;
                }
                if (agency == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Index", new { Id = (int)Session["agencyId"] });
            }
            catch (Exception e)
            {
                return null;
            }
            }
        }
}
