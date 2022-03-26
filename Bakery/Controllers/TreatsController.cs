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
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    // public ActionResult Details(int id)
    // {
    //   var thistreat = _db.Treats
    //     .Include(treat => treat.JoinEntities)
    //     .ThenInclude(join => join.Flavor)
    //     .FirstOrDefault(treat => treat.TreatId == id);
    //   return View(thistreat);
    // }
    public ActionResult Details(int id)
    {
      var model = new FlavorTreatViewModel();
      model.Flavors = new List<Flavor>();
      var flavors = _db.Flavors;
      var thistreat = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      model.ThisTreat = thistreat;
      foreach (Flavor item in flavors)
      {
        model.Flavors.Add(item);
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
    
    // [Authorize]
    // public ActionResult AddTreat (int id)
    // {
    //   var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
    //   ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
    //   return View(thisFlavor);
    // }
    
    [Authorize]
    [HttpPost]
    public ActionResult AddTreat (Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new{ id = flavor.FlavorId});
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", "Flavors", new {id = joinEntry.FlavorId});
    }
  }
}