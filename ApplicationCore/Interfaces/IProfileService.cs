using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using Infrastructure.Entities;

namespace ApplicationCore.Services
{
  public interface IProfileService
  {
    Task<ProfileDTO> GetByIdAsync(string id);
    IEnumerable<AppUser> GetAllProfilesAsync();
  }
}
