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

    public ActionResult Details(int id)
    {
      var thistreat = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thistreat);
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
      return RedirectToAction("Index");
    }
  }
}