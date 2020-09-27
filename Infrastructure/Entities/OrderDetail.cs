using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public ICollection<OrderConv> OrderConvs { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
