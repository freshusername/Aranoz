using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;

namespace HotelsBooking.Models
{
  public class AllProfilesViewModel
  {
    public IEnumerable<ProfileViewModel> profilesList { get; set; }
  }
}
