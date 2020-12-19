using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace HotelsBooking.Models
{
    public class HotelRoomsViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public RoomType Type { get; set; }

        public int RoomId { get; set; }
        public int HotelId { get; set; }
    }
}
