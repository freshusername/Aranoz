using System;
using System.Collections.Generic;
using System.Text;
using static Infrastructure.Enums;

namespace ApplicationCore.DTOs
{
    public class HotelFilterDto
    {
        public string KeyWord { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int HotelsAmount { get; set; }
        public int PagesCount => (int)Math.Ceiling(decimal.Divide(HotelsAmount, PageSize));

        public string Location { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public int? MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public Season? Season { get; set; }
    }
}
