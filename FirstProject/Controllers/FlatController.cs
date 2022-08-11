using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.ViewModels;

namespace FirstProject.Controllers
{
    public class FlatController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();

        // GET: Flat
        public ActionResult Index()
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
            catch(Exception ex)
            {
                return null;
            }
        }


        // GET: Flats/Details/5
        public ActionResult Details(int? id)
        {
            Flat flat = db.Flats.Find(id);          
            return View(flat);

        }

        //GET: Flats/Create
        public ActionResult Create()
        {
            
             var buildings = new SelectList(db.Buildings.ToList(), "Id", "Name");
            ViewData["BuildingsBag"] = buildings;
            
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "BuildingName,BuildingAddress,Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished")] CreateFlatViewModel createFlatViewModel)
        {
            
                Building building = new Building();
                building.Address = createFlatViewModel.BuildingAddress;
                building.Name = createFlatViewModel.BuildingName;
                Flat flat = new Flat();
                flat.Building = building;
                flat.Number = createFlatViewModel.Number;
                flat.Furnished = createFlatViewModel.Furnished;
                flat.Parking = createFlatViewModel.Parking;
                flat.PetAllowed = createFlatViewModel.PetAllowed;
                flat.Bedroom = createFlatViewModel.Bedroom;
                flat.BillsIncluded = createFlatViewModel.BillsIncluded;
                flat.Floor = createFlatViewModel.Floor;
                db.Flats.Add(flat);
                db.SaveChanges();

                return View(flat);
           
        }



        // GET: Flats/Edit/5

        public ActionResult Edit(int? id)

        {
            Flat flat = db.Flats.Find(id);
            
            return View(flat);
        }
        //POST: Flats/Edit/5
        [HttpPost]

        public ActionResult Edit([Bind(Include = "Id,BuildingId,Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished")] Flat flat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(flat);

        }

        //GET: Flats/Delete/5
        public ActionResult Delete(int? id)
        {
            Flat flat = db.Flats.Find(id);
           
            return View(flat);
        }


        //Post:Flats/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Flat flat = db.Flats.Find(id);
            db.Flats.Remove(flat);
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