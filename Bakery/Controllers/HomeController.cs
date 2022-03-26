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

    // [HttpGet("/")]
    // public async Task<IActionResult> Index(string searchString)
    // {
    // var flavorsTreats = from m in _db.FlavorTreat
    //   .Include(m => m.Treat)
    //   .Include(m => m.Flavor)
    //   select m;
    //   {
    //   if (!String.IsNullOrEmpty(searchString))  
    //     flavorsTreats = flavorsTreats.Where(s => s.Flavor.Name!.Contains(searchString));
    //     flavorsTreats = flavorsTreats.Where(s => s.Treat.Name!.Contains(searchString));
    //   }

    // return View(await flavorsTreats.ToListAsync());
    // }

    // [HttpGet("/")]
    // public async Task<IActionResult> Index(string searchString)
    // {
    // var flavors = from m in _db.Flavors
    //   select m;
    //   {
    //   if (!String.IsNullOrEmpty(searchString))  
    //     flavors = flavors.Where(s => s.Name!.Contains(searchString));
    //   }

    // return View(await flavors.ToListAsync());
    // }

    [HttpGet("/")]
    public async Task<IActionResult> Index(string searchString)
    {
    var flavors = from m in _db.Flavors
      select m;
    var treats = from m in _db.Treats
      select m; 
      {
      if (!String.IsNullOrEmpty(searchString))  
        flavors = flavors.Where(s => s.Name!.Contains(searchString));
        treats = treats.Where(s => s.Name!.Contains(searchString));
        await flavors.ToListAsync();
        await treats.ToListAsync();
      }
    Dictionary<string, object> flavorstreats = new Dictionary<string, object>();
    flavorstreats.Add("flavors", flavors);
    flavorstreats.Add("treats", treats);
    return View(flavorstreats);
    }
  }
}