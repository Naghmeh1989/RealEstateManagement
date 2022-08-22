﻿using Data;
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
    //Samte GET e controller bayad ino benevisi:
   // var agencies = new SelectList(dn.Agencies.ToList(), "ID", "Name");
    //ViewData["AgenciesBag"] = agencies;


//Same view baraye dorost kardan drop down list bayad ino dakhele td benevisi:



 //<td> @Html.DropDownListFor("Agencies", (IEnumerable
       // <SelectListItem>) ViewData["AgenciesBag"]) </td>
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tenant tenant = db.Tenants.Find(id);
                if (tenant == null)
                {
                    return HttpNotFound();
                }
                return View(tenant);
            }
        }
        //POST: Tenants/Edit/5
        [HttpPost]

        public ActionResult Edit([Bind(Include = "Id,UserId,Name,LastName,ContractId")]Tenant tenant)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tenant).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(tenant);
            }

        }

        //GET: Tenants/Delete/5
        //[HttpGet, ActionName("Delete")]
        //public ActionResult Delete(int? id)
        //{
        //    if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
        //    {
        //        return RedirectToAction("Login", "Users");
        //    }
        //    else
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Tenant tenant = db.Tenants.Find(id);
        //        if (tenant == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(tenant);
        //    }
        //}

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
                Tenant tenant = db.Tenants.Find(id);
                db.Tenants.Remove(tenant);
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

        
    }
}