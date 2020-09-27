using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HotelConv
    {
        public int Id { get; set; }
        public AdditionalConv AdditionalConv { get; set; }
        public Hotel Hotel { get; set; }
        public decimal Price { get; set; }
    }
}
