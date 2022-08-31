using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FirstProject.ViewModels;
using FirstProject.Business;

namespace FirstProject.Controllers
{
    public class BuildingController : Controller
    {
        private FirstProjectEntities db = new FirstProjectEntities();
        private LoginBusiness loginRestriction = new LoginBusiness();
        // GET: Building
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
                    IQueryable<BuildingViewModel> buildingViewModel = db.Buildings.Include(x => x.Flats).Select(buildingObj => new BuildingViewModel
                    {
                        Id = buildingObj.Id,
                        Name = buildingObj.Name,
                        Address = buildingObj.Address,
                        NumberOfFlats = (int)buildingObj.NumberOfFlats
                    });
                    return View(buildingViewModel);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                 return RedirectToAction("Login", "Users");
            }
                else
                {
                    try
                    {
                        var flatsAndBuildingViewModel = db.Buildings.Where(buildingObj => buildingObj.Id == id).Include(buildingObj => buildingObj.Flats).Select(buildingObj => new BuildingViewModel
                        {
                            Id = buildingObj.Id,
                            Flats = buildingObj.Flats.ToList(),
                            Address = buildingObj.Address,
                            Name = buildingObj.Name,
                            NumberOfFlats = (int)buildingObj.NumberOfFlats,
                        }).First();
                        return View(flatsAndBuildingViewModel);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
        }

        //GET: Buildings/Create
        public ActionResult Create()
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Address,NumberOfFlats")] BuildingViewModel buildingViewModel)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    Building buildingObj = new Building();
                    buildingObj.Name = buildingViewModel.Name;
                    buildingObj.Address = buildingViewModel.Address;
                    buildingObj.NumberOfFlats = buildingViewModel.NumberOfFlats;

                    db.Buildings.Add(buildingObj);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        // GET: Buildings/CreateFlat/5
        public ActionResult CreateFlat(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return RedirectToAction("Create", "Flat", new { buildingId = id });
            }
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var editBuilding = db.Buildings.Include(buildingObj => buildingObj.Flats).Where(buildingObj => buildingObj.Id == id).Select(buildingObj => new EditBuildingViewModel
                {
                    Name = buildingObj.Name,
                    Address = buildingObj.Address,
                    NumberOfFlats = buildingObj.NumberOfFlats,  
                    Flats = (List<Flat>)buildingObj.Flats.ToList(),
            }).First();
                return View(editBuilding);
            }
        }
        public ActionResult EditFlat(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return RedirectToAction("Edit", "Flat", new { Id = id });
            }
        }

        //POST: Buildings/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Number,Floor,Bedroom,Parking,PetAllowed,BillsIncluded,Furnished,Name,Address,NumberOfFlats")] EditBuildingViewModel editBuildingViewModel ,int id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var editBuilding = db.Buildings.Where(buildingObj => buildingObj.Id == id).First();
                editBuilding.Address = editBuildingViewModel.Address;
                editBuilding.Name = editBuildingViewModel.Name;
                editBuilding.NumberOfFlats = editBuildingViewModel.NumberOfFlats;

                db.Entry(editBuilding).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //GET: Buildings/Delete/5
        [HttpGet, ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (loginRestriction.IsRestricted((int?)Session["agencyId"]) == true)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                try
                {
                    Building building = db.Buildings.Find(id);
                    db.Buildings.Remove(building);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    return null;
                }
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