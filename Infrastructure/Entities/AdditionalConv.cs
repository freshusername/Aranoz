using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class AdditionalConv
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoomConv> RoomConvs { get; set; }
        public ICollection<HotelConv> HotelConvs { get; set; }
    }
}
