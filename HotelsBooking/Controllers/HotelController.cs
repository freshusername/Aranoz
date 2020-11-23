using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using HotelsBooking.Models;
using HotelsBooking.Models.Hotel;
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
        public IActionResult ShowHotels(HotelFilterDto HotelFilterDto)
        {
            var hotels = _hotelManager.GetHotels(HotelFilterDto);
            var model = new FilteredHotelsViewModel
            {
                Hotels = hotels,
                HotelFilterDto = HotelFilterDto
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddHotel(HotelDTO hotel)
        {
            _hotelManager.Create(hotel);
            return RedirectToAction("ShowHotels", "Hotel");
        }

        public IActionResult AddHotel()
        {
            return View();
        }

        public async Task<IActionResult> HotelMain(int hotelId)
        {
            HotelDTO hotel = await _hotelManager.GetHotelById(hotelId);
            return View(hotel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
