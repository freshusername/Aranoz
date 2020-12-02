using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class HotelsViewModel
    {
        public IEnumerable<CreateOrEditHotelViewModel> hotels { get; set; }
        public HotelFilterDto HotelFilterDto { get; set; }
    }
}
