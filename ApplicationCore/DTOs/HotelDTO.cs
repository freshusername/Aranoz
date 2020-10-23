using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Infrastructure.Enums;

namespace ApplicationCore.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }

        [Display(Name = "Hotel's name")]
        public string Name { get; set; }

        [Display(Name = "Located in")]
        public string Location { get; set; }

        [Display(Name = "Season")]
        public Season Season { get; set; } 

        public ICollection<HotelPhoto> HotelPhotos { get; set; }

    }
}
