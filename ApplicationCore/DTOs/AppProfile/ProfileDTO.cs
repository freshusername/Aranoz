using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs.AppProfile
{
  public class ProfileDto
  {
    public string Id { get; set; }
    public List<string> Roles { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }
}
