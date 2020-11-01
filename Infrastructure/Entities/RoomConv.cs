using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class RoomConv
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int AdditionalConvId { get; set; }
        public AdditionalConv AdditionalConv { get; set; }

        public int HotelRoomId { get; set; }
        public HotelRoom HotelRoom { get; set; }
    
    }
}
