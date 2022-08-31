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
        private LoginBusiness loginRestriction = new LoginBusiness();
        // GET: Contract
        public ActionResult Index()
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else { 
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
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
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
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
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
            if (loginRestriction.IsRestricted((int)Session["agencyId"]) == true)
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
        // GET: Contracts/CreateRentPayment/5

        public ActionResult CreateRentPayment(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return RedirectToAction("Create", "RentPayment", new { contractId = id });
            }
        }


        // GET: Contracts/Edit/5

        public ActionResult Edit(int? id)

        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var editContract = db.Contracts.Include(x => x.Flat).Where(x => x.Id == id).Select(contractObj => new EditContractViewModel
                {
                    FlatNumber = contractObj.Flat.Number,
                    BuildingName = contractObj.Flat.Building.Name,
                    TenantFirstName = contractObj.Tenant.FirstName,
                    TenantLastName = contractObj.Tenant.LastName,
                    StartDate = contractObj.StartDate,
                    EndDate = contractObj.EndDate,
                    RentPayDay = contractObj.RentPaymentDay,
                    RentAmount = contractObj.RentAmount,

                }).First();
                return View(editContract);
            }
        }
        //POST: Contracts/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "FlatNumber,BuildingName,TenantFirstName,TenantLastName,RentAmount,RentPayDay,StartDate,EndDate")] EditContractViewModel editContractViewModel,int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {

                var editContract = db.Contracts.Include(x => x.Tenant).Include(x => x.Flat).Where(x => x.Id == id).First();
                editContract.StartDate = editContractViewModel.StartDate;
                editContract.EndDate = editContractViewModel.EndDate;
                editContract.RentAmount = editContractViewModel.RentAmount;
                editContract.Tenant.FirstName = editContractViewModel.TenantFirstName;
                editContract.Tenant.LastName = editContractViewModel.TenantLastName;
                editContract.Flat.Number = editContractViewModel.FlatNumber;
                editContract.Flat.Building.Name = editContractViewModel.BuildingName;
               
                    db.Entry(editContract).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                
            }

        }
        //GET: Contracts/EditRentPayment/5
        public ActionResult EditRentPayment(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return RedirectToAction("Edit", "RentPayment", new { Id = id });
            }
        }
        //GET: Contracts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
        //    {
        //        return RedirectToAction("Login", "Users");
        //    }
        //    else
        //    {

        //        Contract contract = db.Contracts.Find(id);

        //        return View(contract);
        //    }
        //}
        //Post:Contracts/Delete/5
        [HttpGet, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
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