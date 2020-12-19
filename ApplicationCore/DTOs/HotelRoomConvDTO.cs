using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class HotelRoomConvDTO
    {
        public int Id { get; set; }
        public string ConvName { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int HotelRoomId { get; set; }
        public int AdditionalId { get; set; }
    }
}
