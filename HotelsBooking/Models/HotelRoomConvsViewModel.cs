using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class HotelRoomConvsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ConvName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int HotelRoomId { get; set; }
    }
}
