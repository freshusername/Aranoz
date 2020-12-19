using System;
using System.Collections.Generic;
using System.Text;
using static Infrastructure.Enums;

namespace ApplicationCore.DTOs
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public RoomType Type { get; set; }

        public int RoomId { get; set; }
        public int HotelId { get; set; }
    }
}
