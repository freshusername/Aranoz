using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace HotelsBooking.Models
{
    public class HotelViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Hotel's name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Located in")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Season")]
        public Season Season { get; set; }

        //min price for room in every hotel
        [Display(Name = "Prices start at")]
        public decimal RoomMinPrice { get; set; }
    }
}
