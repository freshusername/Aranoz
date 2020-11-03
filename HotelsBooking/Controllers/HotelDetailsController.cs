using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsBooking.Controllers
{
    public class HotelDetailsController : Controller
    {
        private readonly IHotelManager _hotelManager;
        public HotelDetailsController(IHotelManager hotelManager)
        {
            this._hotelManager = hotelManager;
        }
        // GET: HotelDetails
        public ActionResult Index()
        {
            IEnumerable<HotelDTO> hotels = _hotelManager.GetHotels();
            return View(hotels);
        }

        // GET: HotelDetails/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HotelDTO hotel =  await _hotelManager.GetHotelById(id);
            return View(hotel);
        }
    }
}