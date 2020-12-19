using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Infrastructure.Enums;

namespace HotelsBooking.Models
{
    public class CreateOrEditHotelViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public Season Season { get; set; }
        public List<IFormFile> HotelPhotos { get; set; }
    }
}
