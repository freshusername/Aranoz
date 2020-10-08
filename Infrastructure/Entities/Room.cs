using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<HotelRoom> HotelRooms { get; set; }
    }
}
