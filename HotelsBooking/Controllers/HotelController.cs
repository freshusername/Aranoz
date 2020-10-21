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
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            this._hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult ShowHotels()
        {
            var hotels = _hotelService.GetHotels();
            return View(hotels);
        }

        [HttpPost]
        public IActionResult AddHotel(HotelDto hotel)
        {
            _hotelService.Insert(hotel);
            return RedirectToAction("ShowHotels", "Hotel");
        }

        public IActionResult AddHotel()
        {
            return View();
        }

        public int GetHotelsCount(string searchValue)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }
            //if (!User.Identity.IsAuthenticated)   // when we will have users with role claims
            //{
            //    return 0;
            //}
            return _hotelService.GetHotelCount(searchValue);
        }

        public IEnumerable<HotelDto> Get(int page, int countOnPage, string searchValue)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            //if (User.Identity.IsAuthenticated && User.IsInRole("Moderator"))    //again, when we will have users with role claims
            //{
            //    var moderatorId = moderatorManager.GetThisModerator(this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
            //    return _hotelService.GetHotels(page, countOnPage, searchValue);
            //}
            else
            {
                return _hotelService.GetHotels(page, countOnPage, searchValue);
            }
            return _hotelService.GetHotels(page, countOnPage, searchValue);
        }

        public IActionResult HotelMain(int hotelId)
        {
            var hotel = _hotelService.Get(hotelId);
            return View(hotel);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
