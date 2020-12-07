using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class AdminPaginationDTO : ICloneable
    {
        public string KeyWord { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Amount { get; set; }
        public int PagesCount => (int)Math.Ceiling(decimal.Divide(Amount, PageSize));

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage <= PagesCount - 1;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
