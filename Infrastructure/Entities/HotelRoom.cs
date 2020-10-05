using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HotelRoom
    {
        public int Id { get; set; }             
        public decimal Price { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<RoomConv> RoomConvs { get; set; }
    }
}
