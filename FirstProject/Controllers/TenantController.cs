using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;




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

        // GET: Tenants
        public ActionResult Index()
        {
            try
            {
                var tenantViewModel = db.Tenants.Include(tenantObj => tenantObj.Contracts).Select(tenantObj => new IndexTenantViewModel
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
        // GET: Tenants/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
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

        //GET: Tenants/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Password,Username,TenantLastName,TenantFirstName")] CreateTenantViewModel createTenantViewModel)
        {
            try
            {
               

                User user = new User();
                user.Username = createTenantViewModel.UserName;
              
                user.Password = createTenantViewModel.Password;
                Tenant tenantObj = new Tenant();
                
                tenantObj.FirstName = createTenantViewModel.TenantFirstName;
                tenantObj.LastName = createTenantViewModel.TenantLastName;
                
                db.Tenants.Add(tenantObj); 
                db.SaveChanges();

                return View(tenantObj);
            }
            catch(Exception ex)
            {
                return null;
            }
    }

        // GET: Tenants/Edit/5
    
        public ActionResult Edit(int? id)
    
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
        //POST: Tenants/Edit/5
        [HttpPost]

        public ActionResult Edit([Bind(Include = "Id,UserId,Name,LastName,ContractId")]Tenant tenant)
        {
         if (ModelState.IsValid)
        {
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

        }
        return View(tenant);

        }

        //GET: Tenants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if(tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        //Post:Tenants/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            db.Tenants.Remove(tenant);
            db.SaveChanges();
            return RedirectToAction ("Index");
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