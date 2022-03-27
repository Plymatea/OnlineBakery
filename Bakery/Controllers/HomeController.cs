using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Bakery.Models;

namespace Bakery.Controllers
{
  public class HomeController : Controller
  {
    private readonly BakeryContext _db;

    public HomeController( BakeryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index(string searchString)
    {
      var model = new FlavorTreatViewModel();
      model.Flavors = new List<Flavor>();
      model.Treats = new List<Treat>();
      var flavors = from m in _db.Flavors
        select m;
      var treats = from m in _db.Treats
        select m;
      if (!String.IsNullOrEmpty(searchString))  
      {
        flavors = flavors.Where(s => s.Name!.Contains(searchString));
        treats = treats.Where(s => s.Name!.Contains(searchString));
      }
      foreach (Flavor item in flavors)
      {
        model.Flavors.Add(item);
      }
      foreach (Treat item in treats)
      {
        model.Treats.Add(item);
      }
      ViewBag.PageTitle = "WELCOME";
      return View(model);
    }
  }
}