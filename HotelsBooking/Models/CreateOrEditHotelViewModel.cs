using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace HotelsBooking.Models
{
    public class CreateOrEditHotelViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name{ get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public Season Season { get; set; }
    }
}
