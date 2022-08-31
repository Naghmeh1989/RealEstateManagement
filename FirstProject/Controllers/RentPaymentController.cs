﻿using Data;
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
                var indexRentPayment = db.RentPayments.Include(x => x.Contract).Select(rentPaymentObj => new IndexRentPaymentViewModel
                {
                    BuildingName = rentPaymentObj.Contract.Flat.Building.Name,
                    FlatNumber = rentPaymentObj.Contract.Flat.Number,
                    RentAmount = rentPaymentObj.Contract.RentAmount,
                    PaymentDay = rentPaymentObj.Contract.RentPaymentDay,
                    PaymentDate = (DateTime)rentPaymentObj.PaymentDate,
                    IsPaid = rentPaymentObj.IsPaid

                });
                return View(indexRentPayment);
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
                    Contract contract = new Contract();
                    contract.Id = createRentPaymentViewModel.ContractId;
                    RentPayment rentPayment1 = new RentPayment();
                    rentPayment1.PaymentDate = (DateTime)createRentPaymentViewModel.PaymentDate;
                    rentPayment1.IsPaid = createRentPaymentViewModel.IsPaid;
                    rentPayment1.Contract = contract;
                    db.RentPayments.Add(rentPayment1);
                    db.SaveChanges();
                    return View(rentPayment1);
                }
                catch(Exception ex)
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
                
                }).First();
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
                    var editRentPayment = db.RentPayments.Where(x => x.Id == id).First();
                    editRentPayment.PaymentDate = (DateTime)editRentPaymentViewModel.PaymentDate;
                    editRentPayment.IsPaid = editRentPaymentViewModel.IsPaid;


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


    //GET : RentPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                RentPayment rentPayment = db.RentPayments.Find(id);
                return View(rentPayment);
            }

        }
        //Post: RentPayments/Delete/5
        [HttpGet, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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