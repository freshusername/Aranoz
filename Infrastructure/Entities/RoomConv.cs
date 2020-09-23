using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class RoomConv
    {
        public int Id { get; set; }
        public AdditionalConv AdditionalConv { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public decimal Price { get; set; }
    }
}
