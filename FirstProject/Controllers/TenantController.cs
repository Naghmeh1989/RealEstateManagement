using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;
using FirstProject.Business;

namespace FirstProject.Controllers
{
    public class TenantController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginBusiness loginRestriction = new LoginBusiness();

        // GET: Tenants
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
                    var tenantViewModel = db.Tenants.Select(tenantObj => new IndexTenantViewModel
                    {
                        TenantId = tenantObj.Id,
                        TenantFirstName = tenantObj.FirstName,
                        TenantLastName = tenantObj.LastName,
                    });
                    return View(tenantViewModel);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        // GET: Tenants/Details/5
        public ActionResult Details(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var tenantDetails = db.Tenants.Where(tenantObj => tenantObj.Id == id).Select(tenantObj => new DetailsTenantViewModel
                {
                    TenantFirstName=tenantObj.FirstName,
                    TenantLastName=tenantObj.LastName,
                }).First();
                return View(tenantDetails);
            }
        }

        //GET: Tenants/Create
        public ActionResult Create()
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "TenantLastName,TenantFirstName")] CreateTenantViewModel createTenantViewModel)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    Tenant tenant = new Tenant();
                    tenant.FirstName = createTenantViewModel.TenantFirstName;
                    tenant.LastName = createTenantViewModel.TenantLastName;
                
                    db.Tenants.Add(tenant);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
    }

        // GET: Tenants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var editTenant = db.Tenants.Where(tenantObj => tenantObj.Id == id).Select(tenantObj => new EditTenantViewModel
                {
                    TenantFirstName = tenantObj.FirstName,
                    TenantLastName = tenantObj.LastName,
                }).First();
                return View(editTenant);
            }
        }
        //POST: Tenants/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TenantFirstName,TenantLastName")]EditTenantViewModel editTenantViewModel,int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var tenant = db.Tenants.Where(tenantObj => tenantObj.Id==id).First();
                tenant.FirstName = editTenantViewModel.TenantFirstName;
                tenant.LastName = editTenantViewModel.TenantLastName;
                
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //Post:Tenants/Delete/5
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
                    Tenant tenant = db.Tenants.Find(id);
                    db.Tenants.Remove(tenant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
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