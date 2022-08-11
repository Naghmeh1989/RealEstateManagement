using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Data;
using FirstProject.ViewModels;

namespace FirstProject.Controllers
{
    public class ContractController : Controller
    { 
        private FirstProjectEntities db = new FirstProjectEntities();
        // GET: Contract
        public ActionResult Index()
        {
            return View(db.Contracts.ToList());
        }

        // GET:/Details/1
        public ActionResult Details(int? id)
        {
            Contract contract = db.Contracts.Find(id);
            return View(contract);

        }

        // GET:/Create
        //public ActionResult
        public ActionResult Create()
        {
            var flats = new SelectList(db.Flats.ToList(), "Id", "Id");
            ViewData["FlatsBag"] = flats;
            var agencies = new SelectList(db.Agencies.ToList(), "Id", "Id");
            ViewData["AgenciesBag"] = agencies;
            var buildings = new SelectList(db.Buildings.ToList(), "Id", "Address");
            ViewData["BuildingsBag"] = buildings;
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "FlatId,TenantFirstName,TenantLastName,AgencyID,RentAmount,RentPayDay,StartDate,EndDate")] CreateContractViewModel createContractViewModel)
        {
            try
            {
                Tenant tenant = new Tenant();
                tenant.Name = createContractViewModel.TenantFirstName;
                tenant.LastName = createContractViewModel.TenantLastName;

                Contract contract = new Contract();
                //contract.StartDate = createContractViewModel.StartDate;
                contract.EndDate = createContractViewModel.EndDate;
                contract.RentPaymentDay = createContractViewModel.RentPayDay;
                contract.RentAmount = createContractViewModel.RentAmount;
                db.Contracts.Add(contract);
                db.SaveChanges();

                return View(contract);
            }
            catch(Exception ex)
            {
                return null;
            }

        }


        // GET: Contracts/Edit/5

        public ActionResult Edit(int? id)

        {
            Contract contract = db.Contracts.Find(id);
            return View(contract);
        }
        //POST: Contracts/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenantId,AgencyId,FlatId,RentAmount,RentPayDay,StartDate,EndDate")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(contract);

        }
        //GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Contract contract = db.Contracts.Find(id);
            
            return View(contract);
        }
        //Post:Contracts/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
            db.SaveChanges();
            return RedirectToAction("Index");
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