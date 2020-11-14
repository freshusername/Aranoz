using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class PagingDto
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int HotelsAmount { get; set; }
        public int PagesCount => (int)Math.Ceiling(decimal.Divide(HotelsAmount, PageSize));
    }
}
