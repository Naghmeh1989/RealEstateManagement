using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;
using FirstProject.Business;
using System.Data;
using System.Net;

namespace FirstProject.Controllers
{
    
     public class AgencyController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginBusiness loginRestriction = new LoginBusiness();

        // GET: Agencies
        public ActionResult Index()
        {
            try
            {
                if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    var agenciesAndUsersViewModels = db.Agencies.Include(ag => ag.User).Select(ag => new IndexAgencyViewModel
                    {
                        AgencyName = ag.Name,
                        AgencyId = ag.Id,
                        UserId = ag.UserId,
                        UserName = ag.User.Username
                    });
                    return View(agenciesAndUsersViewModels);
                }
            }
            catch (Exception ex)
            {
                return null;
            };
        
        }

        // GET : Agencies/Details/1
        public ActionResult Details(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    var agencyDetails = db.Agencies.Include(x => x.User).Where(agencyObj => agencyObj.Id == id).Select(agencyObj => new DetailsAgencyViewModel
                    {
                        AgencyId = agencyObj.Id,
                        AgencyName = agencyObj.Name,
                        UserId = agencyObj.UserId,
                        UserName = agencyObj.User.Username
                    }).First();
                    return View(agencyDetails);
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
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
                Agency agency1 = new Agency();
                agency1.Name = createAgencyViewModel.Name;
                agency1.User = user;

                db.Agencies.Add(agency1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Get : Agencies/Edit
        public ActionResult Edit(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var agency = db.Agencies.Where(agencyObj => agencyObj.Id == id).Select(agencyObj => new EditAgencyViewModel
                {
                    AgencyName = agencyObj.Name
                }).First();
                return View(agency);
            }
        }
        //Post : Agencies/Edit
        [HttpPost]
        public ActionResult Edit([Bind(Include = "AgencyName,Id,UserId")] EditAgencyViewModel editAgencyViewModel, int id)
        {
            try
            {
                if (loginRestriction.IsRestricted((int)Session["agencyId"]) == true)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    var editAgency = db.Agencies.Include(agencyObj => agencyObj.User).Where(agencyObj => agencyObj.Id == id).First();
                    editAgency.Name = editAgencyViewModel.AgencyName;

                    db.Entry(editAgency).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        //Agencies/Delete
        [HttpGet, ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
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