using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Services
{
  public interface IProfileService
  {
    Task<ProfileDto> GetByIdAsync(string id);
    Task<ProfileDto> GetByEmailAsync(string email);
    

    Task<IEnumerable<ProfileDto>> GetAllProfilesAsync();
    Task<OperationDetails> UpdateProfile(ProfileDto model);

    Task<List<string>> GetRoles(string id);
  }
}
