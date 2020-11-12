using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.DTOs
{
    public class AdminOrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
    }
}
