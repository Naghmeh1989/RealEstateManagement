using Data;
using FirstProject.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstProject.Controllers
{
    public class RentPaymentController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginRestriction loginRestriction = new LoginRestriction();

    //GET RentPayments
        public ActionResult Index()
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View(db.RentPayments.ToList());
            }
        }

    //GET : Details/RentPayments/5

        
        public ActionResult Details(int? id)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                RentPayment rentPayment = db.RentPayments.Find(id);
                return View(rentPayment);
            }

        }

    //GET : RentPayment/Create

        public ActionResult Create()
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }
        }

    //POST : RentPayment/Create
     [HttpPost]

        public ActionResult Create([Bind(Include = "Id,IsPaid,ContractId,PaymentDay")] RentPayment rentPayment)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                db.RentPayments.Add(rentPayment);
                db.SaveChanges();
                return View(rentPayment);
            }
        }

    //GET : RentPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                RentPayment rentPayment = db.RentPayments.Find(id);
                return View(rentPayment);
            }
        }

    //POST : RentPayment/Edit/5
        [HttpPut]
        public ActionResult Edit([Bind(Include = "Id,IsPaid,ContractId,PaymentDay")] RentPayment rentPayment)
        {
            if (loginRestriction.IsRestricted() == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(rentPayment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(rentPayment);
            }

        }


    //GET : RentPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted() == true)
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
        public ActionResult DeleteConfirmed(int id)
        {
            if (loginRestriction.IsRestricted() == true)
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