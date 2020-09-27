using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
