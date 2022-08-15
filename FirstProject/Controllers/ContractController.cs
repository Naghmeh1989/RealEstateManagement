using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Data;
using FirstProject.ViewModels;
using FirstProject.Business;

namespace FirstProject.Controllers
{
    public class ContractController : Controller
    { 
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginRestriction loginRestriction = new LoginRestriction();
        // GET: Contract
        public ActionResult Index()
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (Session["userId"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                try
                {
                    var contractViewModel = db.Contracts.Include(x => x.Flat).Select(contractObj => new IndexContractViewModel
                    {
                        BuildingName = contractObj.Flat.Building.Name,
                        FlatNumber = contractObj.Flat.Number,
                        TenantFirstName = contractObj.Tenant.FirstName,
                        TenantLastName = contractObj.Tenant.LastName,
                        StartDate = contractObj.StartDate,
                        EndDate = contractObj.EndDate,
                        PaymentDay = contractObj.RentPaymentDay,
                        RentAmount = contractObj.RentAmount,


                    });
                    return View(contractViewModel);



                }
                catch (Exception ex)
                {
                    return null;
                }
            }


        }

        // GET:/Details/1
        public ActionResult Details(int? id)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    var contract = db.Contracts.Include(x => x.Flat).Include(x => x.Tenant).First(x => x.Id == id);
                    Building building = db.Buildings.Find(contract.Flat.BuildingId);
                    var detailsViewModel = new DetailsContractViewModel

                    {
                        BuildingName = building.Name,
                        FlatNumber = contract.Flat.Number,
                        TenantFirstName = contract.Tenant.FirstName,
                        TenantLastName = contract.Tenant.LastName,
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate,
                        PaymentDay = contract.RentPaymentDay,
                        RentAmount = contract.RentAmount,

                    };

                    return View(detailsViewModel);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        // GET:/Create
        //public ActionResult
        public ActionResult Create()
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var flats = new SelectList(db.Flats.ToList(), "Id", "Id");
                ViewData["FlatsBag"] = flats;


                return View();
            }
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "FlatId,TenantFirstName,TenantLastName,AgencyID,RentAmount,RentPayDay,StartDate,EndDate")] CreateContractViewModel createContractViewModel)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    Tenant tenant = new Tenant();
                    tenant.FirstName = createContractViewModel.TenantFirstName;
                    tenant.LastName = createContractViewModel.TenantLastName;

                    Contract contract = new Contract();
                    contract.Tenant = tenant;
                    contract.StartDate = createContractViewModel.StartDate;
                    contract.EndDate = createContractViewModel.EndDate;
                    contract.RentPaymentDay = createContractViewModel.RentPayDay;
                    contract.RentAmount = createContractViewModel.RentAmount;
                    contract.FlatId = createContractViewModel.FlatId;
                    contract.AgencyId = (int)Session["agencyId"];

                    db.Contracts.Add(contract);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }


        // GET: Contracts/Edit/5

        public ActionResult Edit(int? id)

        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Contract contract = db.Contracts.Find(id);
                return View(contract);
            }
        }
        //POST: Contracts/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenantId,AgencyId,FlatId,RentAmount,RentPayDay,StartDate,EndDate")] Contract contract)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(contract).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(contract);
            }

        }
        //GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {

                Contract contract = db.Contracts.Find(id);

                return View(contract);
            }
        }
        //Post:Contracts/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Contract contract = db.Contracts.Find(id);
                db.Contracts.Remove(contract);
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