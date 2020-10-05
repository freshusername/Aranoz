using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HotelConv
    {
        public int Id { get; set; }
       
        public decimal Price { get; set; }

        public int AdditionalConvId { get; set; }
        public virtual AdditionalConv AdditionalConv { get; set; }

        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
