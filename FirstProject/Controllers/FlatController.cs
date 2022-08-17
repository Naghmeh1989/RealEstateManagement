using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;
using FirstProject.Business;

namespace FirstProject.Controllers
{
    public class FlatController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginBusiness loginRestriction = new LoginBusiness();

        // GET: Flat
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
                    var indexFlatViewModel = db.Flats.Include(x => x.Building).Select(flatObj => new IndexFlatViewModel
                    {
                        BuildingAddress = flatObj.Building.Address,
                        BuildingName = flatObj.Building.Name,
                        Floor = flatObj.Floor,
                        Number = flatObj.Number,
                        Furnished = (bool)flatObj.Furnished,
                        Parking = (bool)flatObj.Parking,
                        PetAllowed = (bool)flatObj.PetAllowed,
                        Bedroom = (int)flatObj.Bedroom



                    });
                    return View(indexFlatViewModel);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        // GET: Flats/Details/5
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
                    var flatDetails = db.Flats.Include(x => x.Building).Where(flatObj => flatObj.Id == id).Select(flatObj => new DetailsFlatViewModel
                    {
                        BuildingAddress = flatObj.Building.Address,
                        BuildingName = flatObj.Building.Name,
                        Floor = flatObj.Floor,
                        Number = flatObj.Number,
                        Bedroom = (int)flatObj.Bedroom,
                        Furnished = (bool)flatObj.Furnished,
                        BillsIncluded = (bool)flatObj.BillsIncluded,
                        Parking = (bool)flatObj.Parking,

                    });
                    return View(flatDetails);
                }
                catch(Exception ex)
                {
                    return null;
                }

            }

        }



        //GET: Flats/Create
        public ActionResult Create(int? buildingId)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                ViewData["BuildingId"] = buildingId;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "BuildingId,Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished")] CreateFlatViewModel createFlatViewModel)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Flat flat = new Flat();
                flat.BuildingId = createFlatViewModel.BuildingId;
                flat.Number = createFlatViewModel.Number;
                flat.Furnished = createFlatViewModel.Furnished;
                flat.Parking = createFlatViewModel.Parking;
                flat.PetAllowed = createFlatViewModel.PetAllowed;
                flat.Bedroom = createFlatViewModel.Bedroom;
                flat.BillsIncluded = createFlatViewModel.BillsIncluded;
                flat.Floor = createFlatViewModel.Floor;
                db.Flats.Add(flat);
                db.SaveChanges();

                return RedirectToAction("Details", "Building", new { id = createFlatViewModel.BuildingId });

            }
        }



        // GET: Flats/Edit/5

        public ActionResult Edit(int? id)

        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Flat flat = db.Flats.Find(id);

                return View(flat);
            }
        }
        //POST: Flats/Edit/5
        [HttpPost]

        public ActionResult Edit([Bind(Include = "Id,BuildingId,Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished")] Flat flat)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(flat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(flat);
            }

        }

        //GET: Flats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Flat flat = db.Flats.Find(id);

                return View(flat);
            }
        }


        //Post:Flats/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Flat flat = db.Flats.Find(id);
                db.Flats.Remove(flat);
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