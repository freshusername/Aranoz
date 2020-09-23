using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Season Season { get; set; }
        public ICollection<HotelRoom> HotelRooms { get; set; }
    }

    public enum Season { Hot, Cold, Demiseason }
}
