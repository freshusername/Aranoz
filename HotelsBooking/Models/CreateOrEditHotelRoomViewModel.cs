using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace HotelsBooking.Models
{
    public class CreateOrEditHotelRoomViewModel
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 10000, ErrorMessage = "Number need to be in range from 1 to 10000")]
        public int Number { get; set; }
        [Required]
        [Range(0, 100000, ErrorMessage = "Price need to be in range from 0 to 100000")]
        public decimal Price { get; set; }
        [Required]
        public RoomType Type { get; set; }
        public int HotelId { get; set; }
    }
}
