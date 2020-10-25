using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsBooking.Controllers
{
    public class HotelDetailsController : Controller
    {
        ApplicationDbContext db;
        public HotelDetailsController(ApplicationDbContext context)
        {
            db = context;
        }
        // GET: HotelDetails
        public ActionResult Index()
        {
            return View(db.Hotels);
        }

        // GET: HotelDetails/Details/5
        public ActionResult Details(int id)
        {
            Hotel hotel = db.Hotels.Include(h => h.HotelRooms)
                                         .ThenInclude(hr => hr.Room)
                                    .Include(h => h.HotelRooms)
                                         .ThenInclude(hr => hr.RoomConvs)
                                    .FirstOrDefault(x => x.Id == id);
            return View(hotel);
        }
    }
}