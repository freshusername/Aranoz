using System;
using System.Collections.Generic;
using System.Text;
using static Infrastructure.Enums;

namespace ApplicationCore.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Season Season { get; set; }

    }
}
