using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;


namespace HotelsBooking.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        [Display(Name = "Order date")]
        public DateTimeOffset OrderDate { get; set; }
        [Display(Name = "Check-in date")]
        public DateTimeOffset CheckInDate { get; set; }
        [Display(Name = "Check-out date")]
        public DateTimeOffset CheckOutDate { get; set; }
        [Display(Name = "Total price")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Hotel name")]
        public string HotelName { get; set; }
        [Display(Name = "Room")]
        public int RoomId { get; set; }
    }
}
