using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    var flavors = from m in _db.Flavors
                select m;
    var treats = from m in _db.Treats
                select m;
      if (!String.IsNullOrEmpty(searchString))
      {
        flavors = flavors.Where(s => s.Name!.Contains(searchString));
        treats = treats.Where(s => s.Name!.Contains(searchString));
        flavors.ToList();
        treats.ToList();
      }
    Dictionary<string, List<Flavor>> flavorsTreats = new Dictionary<string, List<Flavor>>();
    flavorsTreats.Add("flavors", flavors);
    return View(flavorsTreats);
    }
  }
}