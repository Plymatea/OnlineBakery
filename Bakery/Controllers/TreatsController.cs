using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Bakery.Models;

namespace Bakery.Controllers
{
  public class TreatsController : Controller
  {
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
  
    public TreatsController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      if (treat.Name != null)
      {
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
      } else
      {
        return RedirectToAction("Create");
      }
    }

    public ActionResult Details(int id)
    {
      var model = new FlavorTreatViewModel();
      model.Flavors = new List<Flavor>();
      var flavors = _db.Flavors;
      var thisTreat = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      model.ThisTreat = thisTreat;
      List<int> thisTreatFlavorIds = new List<int>();
      foreach (FlavorTreat item in thisTreat.JoinEntities)
      {
        thisTreatFlavorIds.Add(item.FlavorId);
      }
      foreach (Flavor item in flavors)
      {
        if (!thisTreatFlavorIds.Contains(item.FlavorId))
        {
        model.Flavors.Add(item);
        }
      }
      return View(model);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var thistreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thistreat);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit (Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treat.TreatId});
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      var thistreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thistreat);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thistreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thistreat);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }
        
    [Authorize]
    [HttpPost]
    public ActionResult AddFlavor (int flavorId, int treatId)
    {
      if (treatId != 0 && flavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new{ id = treatId});
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      int id = joinEntry.TreatId;
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = id});
    }
  }
}