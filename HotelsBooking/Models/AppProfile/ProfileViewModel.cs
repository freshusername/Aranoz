using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace HotelsBooking.Models.AppProfile
{
  public class ProfileViewModel
  {
    public string Id { get; set; }
    public List<string> Roles { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] ProfileImage { get; set; }
    public string Email { get; set; }

    //public ICollection<Order> Orders { get; set; }    
  }
}
