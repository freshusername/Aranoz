using System;
using System.Collections.Generic;
using System.Text;

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

        // public PagingDto PagingDto { get; set; }
    }
}
