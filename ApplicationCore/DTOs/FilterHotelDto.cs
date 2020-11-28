using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class FilterHotelDto
    {
        public string KeyWord { get; set; }
        public string Location { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
