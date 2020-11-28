using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBooking.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Locations = _context.Hotels.Select(h => h.Location).Distinct().ToList();
            return View();
        }
    }
}