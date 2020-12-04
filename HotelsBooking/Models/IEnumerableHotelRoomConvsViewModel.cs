using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class IEnumerableHotelRoomConvsViewModel
    {
        public IEnumerable<HotelRoomConvsViewModel> roomConvs { get; set; }
        public AdminPaginationDTO PaginationDTO { get; set; }
    }
}
