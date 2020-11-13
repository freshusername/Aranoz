using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs.AppProfile
{
  public class ProfileRoleDto
  {
    public string Id { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] ProfileImage { get; set; }
  }
}
