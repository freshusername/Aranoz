using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models.AppProfile
{
  public class ProfileUpdateViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IFormFile ProfileImage { get; set; }
  }
}
