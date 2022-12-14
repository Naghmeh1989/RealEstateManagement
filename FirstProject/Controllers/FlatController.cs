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
                        FlatId = flatObj.Id,
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

                    }).First();
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
                try
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
                catch(Exception ex)
                {
                    return null;
                }
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
                try
                {

                    var editFlat = db.Flats.Include(x => x.Building).Where(flatObj => flatObj.Id== id).Select(flatObj => new EditFlatViewModel
                    {
                        PetAllowed = (bool)flatObj.PetAllowed,
                        Parking = (bool)flatObj.Parking,
                        Bedroom = (int)flatObj.Bedroom,
                        Floor = (int)flatObj.Floor,
                        Furnished = (bool)flatObj.Furnished,
                        BillsIncluded = (bool)flatObj.BillsIncluded,
                        Number = flatObj.Number,
                    }).FirstOrDefault();
                    return View(editFlat);
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        //POST: Flats/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,BuildingId,Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished")] EditFlatViewModel editFlatViewModel,int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    var editFlat = db.Flats.Include(x => x.Building).Where(x => x.Id == id).First();
                    editFlat.Bedroom = editFlatViewModel.Bedroom;
                    editFlat.Floor = editFlatViewModel.Floor;
                    editFlat.Number = editFlatViewModel.Number;
                    editFlat.BillsIncluded = editFlatViewModel.BillsIncluded;
                    editFlat.Furnished = editFlatViewModel.Furnished;
                    editFlat.PetAllowed = editFlatViewModel.PetAllowed;
                    editFlat.Parking = editFlatViewModel.Parking;

                    db.Entry(editFlat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }
        
        //Post:Flats/Delete/5
        [HttpGet, ActionName("Delete")]
        public ActionResult Delete(int id)
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