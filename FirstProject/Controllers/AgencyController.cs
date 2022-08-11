using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;

namespace FirstProject.Controllers
{
    
     public class AgencyController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();

        //private FirstProjectEntities db;
        //private string name;
        //private int _id;
        //public AgencyController(FirstProjectEntities _db,String _name)
        //{ 
        //    db = _db;
        //    name = _name;
        //}

        //public AgencyController(int id)
        //{
        //    _id = id;

        //}

        // GET: Agencies
        public ActionResult Index()
        {
            try
            {
                //IndexAgencyViewModel model1 = new IndexAgencyViewModel();
                //model1.AgencyId = 1;
                //model1.UserId = 5;

                //IndexAgencyViewModel model2 = new IndexAgencyViewModel { 
                //    UserId = 1, 
                //    AgencyName ="Iran" 
                //};

                var agenciesAndUsersViewModels = db.Agencies.Include(ag => ag.User).Select(ag => new IndexAgencyViewModel
                {
                    AgencyName = ag.Name,
                    AgencyId = ag.Id,
                    UserId = ag.UserId,
                    UserName = ag.User.Username
                });
                
                 return View(agenciesAndUsersViewModels);
            }
            catch (Exception ex) 
            {
                return null;
            };
        }






         

        // GET : Agencies/Details/1

        public ActionResult Details(int? id)
        {
            Agency agency = db.Agencies.Find(id);
            return View(id);
        }
        // Get : Agencies/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST : Agencies/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="UserName,Password,Name")]CreateAgencyViewModel createAgencyViewModel)
        {
            try
            {
                User user = new User();

                user.Username = createAgencyViewModel.UserName;
                user.Password = createAgencyViewModel.Password;
                // db.Users.Add(user);
                // db.SaveChanges();

                Agency agency1 = new Agency();

                agency1.Name = createAgencyViewModel.Name;
                //agency1.UserId = user.Id;
                agency1.User = user;

                db.Agencies.Add(agency1);
                db.SaveChanges();
                return View(agency1);
            }
            catch (Exception ex)
            {
                return null;
            }


          
        }
        //Get : Agencies/Edit
        public ActionResult Edit(int? id)
        {
            Agency agency = db.Agencies.Find(id);
            return View(agency);
        }
        //Post : Agencies/Edit
        [HttpPut]
        public ActionResult Edit([Bind(Include = "Id, Name, UserId")]Agency agency)
        {
            db.Entry(agency).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         
        }
        //Agencies/Delete
        [HttpDelete, ActionName("Delete")]
        public ActionResult Delete(int? id)
        {

            try
            {
                Agency agency = db.Agencies.Find(id);
                db.Agencies.Remove(agency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return null;
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

       

    }
}