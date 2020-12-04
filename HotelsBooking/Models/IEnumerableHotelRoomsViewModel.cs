using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class IEnumerableHotelRoomsViewModel
    {
        public IEnumerable<HotelRoomsViewModel> hotelRooms { get; set; }
        public AdminPaginationDTO PaginationDTO { get; set; }
    }
}
