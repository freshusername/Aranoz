using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }

        [Display(Name = "Hotel's name")]
        public string Name { get; set; }

        [Display(Name = "Located in")]
        public string Location { get; set; }

        [Display(Name = "Season")]
        public Season Season { get; set; } //add reference to Infrastructure? or map Season from model to dto as a string?
    }
}
