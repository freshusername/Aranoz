using System;
using System.Collections.Generic;
using System.Text;
using static Infrastructure.Enums;

namespace ApplicationCore.DTOs
{
    public class HotelFilterDto : ICloneable
    {
        public string KeyWord { get; set; }

        public decimal MinSearchPrice { get; set; }
        public decimal MaxSearchPrice { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int HotelsAmount { get; set; }
        public int PagesCount => (int)Math.Ceiling(decimal.Divide(HotelsAmount, PageSize));

        public decimal MaxAvailRoomPrice { get; set; }
        public decimal MinAvailRoomPrice { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage <= PagesCount - 1;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public string Location { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public int? MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public Season? Season { get; set; }


    }
}
