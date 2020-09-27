using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class OrderConv
    {
        public int Id { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public HotelConv HotelConv { get; set; }
    }
}
