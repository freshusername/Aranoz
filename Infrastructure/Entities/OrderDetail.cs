using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }       
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }


        public int HotelRoomId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }   

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
      
    }
}
