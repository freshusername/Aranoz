using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace Infrastructure.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public Season Season { get; set; }

        public ICollection<HotelPhoto> HotelPhotos { get; set; }
        public ICollection<HotelRoom> HotelRooms { get; set; }
        public IEnumerable<HotelConv> HotelConvs { get; set; }
    }

}
