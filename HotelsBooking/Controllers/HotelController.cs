using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using HotelsBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelsBooking.Controllers
{
    [Route("[controller]/[action]")]
    public class HotelController : Controller
    {
        private readonly IHotelManager _hotelManager;
        public HotelController(IHotelManager hotelManager)
        {
            this._hotelManager = hotelManager;
        }

        [HttpGet]
        public IActionResult ShowHotels()
        {
            var hotels = _hotelManager.GetHotels();
            return View(hotels);
        }
        public IActionResult template()
        {
            var hotels = _hotelManager.GetHotels();

            return View(hotels);
        }

        [HttpPost]
        public IActionResult AddHotel(HotelDto hotel)
        {
            _hotelManager.Insert(hotel);
            return RedirectToAction("ShowHotels", "Hotel");
        }

        public IActionResult AddHotel()
        {
            return View();
        }





        public IActionResult HotelMain(int hotelId)
        {
            HotelDto hotel = _hotelManager.Get(hotelId);
            return View(hotel);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
