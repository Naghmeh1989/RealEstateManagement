using Data;
using FirstProject.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;

namespace FirstProject.Controllers
{
    public class RentPaymentController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginBusiness loginRestriction = new LoginBusiness();

    //GET RentPayments
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
                    var indexRentPayment = db.RentPayments.Include(x => x.Contract).Select(rentPaymentObj => new IndexRentPaymentViewModel
                    {
                        RentPaymentId = rentPaymentObj.Id,
                        BuildingName = rentPaymentObj.Contract.Flat.Building.Name,
                        FlatNumber = rentPaymentObj.Contract.Flat.Number,
                        RentAmount = rentPaymentObj.Contract.RentAmount,
                        PaymentDay = rentPaymentObj.Contract.RentPaymentDay,
                        PaymentDate = (DateTime)rentPaymentObj.PaymentDate,
                        IsPaid = rentPaymentObj.IsPaid


                    });
                    return View(indexRentPayment);
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

    //GET : Details/RentPayments/5
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
                    var detailsRentPayment = db.RentPayments.Include(x => x.Contract).Where(x => x.Id == id).Select(rentPaymentObj => new DetailsRentPaymentViewModel
                    {
                        BuildingName = rentPaymentObj.Contract.Flat.Building.Name,
                        FlatNumber = rentPaymentObj.Contract.Flat.Number,
                        RentAmount = rentPaymentObj.Contract.RentAmount,
                        PaymentDay = rentPaymentObj.Contract.RentPaymentDay,
                        PaymentDate = (DateTime)rentPaymentObj.PaymentDate,
                        IsPaid = rentPaymentObj.IsPaid

                    }).First();
                    return View(detailsRentPayment);
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

    //GET : RentPayment/Create
        public ActionResult Create(int? contractId)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                ViewData["ContractId"] = contractId;
                return View();
            }
        }

    //POST : RentPayment/Create
     [HttpPost]
        public ActionResult Create([Bind(Include = "Id,IsPaid,ContractId,PaymentDate")] CreateRentPaymentViewModel createRentPaymentViewModel)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    RentPayment rentPayment1 = new RentPayment();
                    rentPayment1.PaymentDate = createRentPaymentViewModel.PaymentDate;
                    rentPayment1.IsPaid = createRentPaymentViewModel.IsPaid;
                    rentPayment1.ContractId = createRentPaymentViewModel.ContractId;
                    db.RentPayments.Add(rentPayment1);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Contract", new { id = createRentPaymentViewModel.ContractId });

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    //GET : RentPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var editRentPayment = db.RentPayments.Include(x => x.Contract).Where(x => x.Id == id).Select(rentPaymentObj => new EditRentPaymentViewModel
                { 
                    IsPaid = rentPaymentObj.IsPaid,
                    PaymentDate = (DateTime)rentPaymentObj.PaymentDate
                
                }).FirstOrDefault();
                return View(editRentPayment);
            }
        }

    //POST : RentPayment/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IsPaid,ContractId,PaymentDate")] EditRentPaymentViewModel editRentPaymentViewModel, int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    var editRentPayment = db.RentPayments.Include(x => x.Contract).Where(x => x.Id == id).First();

                    editRentPayment.IsPaid = editRentPaymentViewModel.IsPaid;
                    editRentPayment.PaymentDate = editRentPaymentViewModel.PaymentDate;

                    db.Entry(editRentPayment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

  
        //Post: RentPayments/Delete/5
        [HttpGet, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                RentPayment rentPayment = db.RentPayments.Find(id);
                db.RentPayments.Remove(rentPayment);
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