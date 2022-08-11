using Data;
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
        public FirstProjectEntities db = new FirstProjectEntities();

    //GET RentPayments
        public ActionResult Index()
        {
            return View(db.RentPayments.ToList());
        }

    //GET : Details/RentPayments/5

        
        public ActionResult Details(int? id)
        {
            RentPayment rentPayment = db.RentPayments.Find(id);
            return View(rentPayment);

        }

    //GET : RentPayment/Create

        public ActionResult Create()
        {
            return View();
        }

    //POST : RentPayment/Create
     [HttpPost]

        public ActionResult Create([Bind(Include = "Id,IsPaid,ContractId,PaymentDay")] RentPayment rentPayment)
        {
            db.RentPayments.Add(rentPayment);
            db.SaveChanges();
            return View(rentPayment);   
        }

    //GET : RentPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            RentPayment rentPayment =db.RentPayments.Find(id);
            return View(rentPayment);
        }

    //POST : RentPayment/Edit/5
        [HttpPut]
        public ActionResult Edit([Bind(Include = "Id,IsPaid,ContractId,PaymentDay")] RentPayment rentPayment)
        {
            if(ModelState.IsValid)
            {
                db.Entry(rentPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentPayment);

        }


    //GET : RentPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            RentPayment rentPayment = db.RentPayments.Find(id);
            return View(rentPayment);

        }
    //Post: RentPayments/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            RentPayment rentPayment = db.RentPayments.Find(id);
            db.RentPayments.Remove(rentPayment);
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