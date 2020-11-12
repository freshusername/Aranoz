using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class FilterHotelDto
    {
        public string KeyWord { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
