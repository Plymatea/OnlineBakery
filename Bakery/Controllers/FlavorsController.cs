using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Bakery.Models;

namespace Bakery.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
  
    [Authorize]
    public ActionResult Create()
    {
      ViewBag.PageTitle = "CREATE YOUR ART";
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      if (flavor.Name != null)
      {
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
      } else
      {
        return RedirectToAction("Create");
      }
    }

    public ActionResult Details(int id)
    {
      ViewBag.PageTitle = "ALL THE DEETS";
      var model = new FlavorTreatViewModel();
      model.Treats = new List<Treat>();
      var treats = _db.Treats;
      var thisFlavor = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      model.ThisFlavor = thisFlavor;
      List<int> thisFlavorTreatIds = new List<int>();
      foreach (FlavorTreat item in thisFlavor.JoinEntities)
      {
        thisFlavorTreatIds.Add(item.TreatId);
      }
      foreach (Treat item in treats)
      {
        if (!thisFlavorTreatIds.Contains(item.TreatId))
        {
        model.Treats.Add(item);
        }
      }
      return View(model);
    }

    [Authorize]
    public ActionResult Edit (int id)
    {
      ViewBag.PageTitle = "MISTAKES WERE MADE";
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit (Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new{ id = flavor.FlavorId});
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      ViewBag.PageTitle = "IRREVERSABLE";
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }
    [Authorize]
    [HttpPost]
    public ActionResult AddTreat (int flavorId, int treatId)
    {
      if (treatId != 0 && flavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new{ id = flavorId});
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeleteTreat (int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      int id = joinEntry.FlavorId;
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = id});
    }
  }
}
