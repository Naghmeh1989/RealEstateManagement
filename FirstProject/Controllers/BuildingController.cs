using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FirstProject.ViewModels;

namespace FirstProject.Controllers
{
    public class BuildingController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();

        // GET: Building
        public ActionResult Index()
        {
            try
            {
                IQueryable<BuildingViewModel> buildingViewModel = db.Buildings.Include(x => x.Flats).Select(buildingObj => new BuildingViewModel
                {
                    Name = buildingObj.Name,
                    Address = buildingObj.Address,
                    NumberOfFlats = (int)buildingObj.NumberOfFlats
                });
                return View(buildingViewModel);
            }
            catch(Exception ex)
            {
                return null;
            }
        }



        // GET: Buildings/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var flatsAndBuildingViewModel = db.Buildings.Where(buildingObj => buildingObj.Id == id).Include(buildingObj => buildingObj.Flats).Select(buildingObj => new BuildingViewModel
                {
                    Flats = buildingObj.Flats.ToList(),
                    Address = buildingObj.Address,
                    Name = buildingObj.Name,
                    NumberOfFlats = (int)buildingObj.NumberOfFlats,
                }).First();
                return View(flatsAndBuildingViewModel);
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        //GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Address,NumberOfFlats")] BuildingViewModel buildingViewModel)
        {
            try
            {
                Building buildingObj = new Building();
                buildingObj.Name = buildingViewModel.Name;
                buildingObj.Address = buildingViewModel.Address;
                buildingObj.NumberOfFlats = buildingViewModel.NumberOfFlats;


                db.Buildings.Add(buildingObj);
                db.SaveChanges();

                return View(buildingObj);
            }
            catch(Exception ex)
            {
                return null;

            }
        }

        // GET: Buildings/CreateFlat/5

        public ActionResult CreateFlat(int? id)

        {

            Building building = db.Buildings.Find(id);

            return View(building);
        }

        [HttpPost]
        public ActionResult CreateFlat([Bind(Include = "Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished")] CreateFlatViewModel createFlatViewModel)
        {
            try
            {

                Flat flat = new Flat();
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
            catch(Exception ex)
            {
                return null;
            }

        }




        // GET: Buildings/Edit/5

        public ActionResult Edit(int? id)

        {

            Building building = db.Buildings.Find(id);

            return View(building);
        }
        //POST: Buildings/Edit/5
        [HttpPost]

        public ActionResult Edit([Bind(Include = "Id,Name,Address,NumberOfFlats")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(building);

        }

        //GET: Buildings/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Building building = db.Buildings.Find(id);

            return View(building);
        }

        //Post:Buildings/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            db.Buildings.Remove(building);
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