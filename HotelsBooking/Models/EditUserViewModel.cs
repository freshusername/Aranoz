using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [RegularExpression(@"^\+[0-9]{11,12}$", ErrorMessage = "Wrong phone number")]
        public string Phone { get; set; }
    }
}
