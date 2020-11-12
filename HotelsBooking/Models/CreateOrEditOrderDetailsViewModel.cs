using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelsBooking.Models
{
    public class CreateOrEditOrderDetailsViewModel:IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public DateTimeOffset CheckInDate { get; set; }
        [Required]
        public DateTimeOffset CheckOutDate { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public int RoomId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CheckInDate > CheckOutDate)
            {
                yield return new ValidationResult("Check-out date must be greater than Check-in date",
                                                    new[] { "CheckOutDate" });
            }
        }
    }
}
