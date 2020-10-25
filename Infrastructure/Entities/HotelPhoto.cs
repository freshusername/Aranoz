using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class HotelPhoto
    {
        public int Id { get; set; }
        public byte[] image { get; set; }

        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
