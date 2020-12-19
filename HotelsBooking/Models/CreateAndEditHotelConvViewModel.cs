using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class CreateAndEditHotelConvViewModel
    {
        public CreateOrEditHotelConvViewModel model { get; set; }
        public IEnumerable<AdditionalConvDTO> additionalConvs { get; set; }
    }
}
