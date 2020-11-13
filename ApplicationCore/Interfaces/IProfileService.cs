using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Services
{
  public interface IProfileService
  {
    Task<ProfileDTO> GetByIdAsync(string id);
    Task<ProfileDTO> GetByEmailAsync(string email);

    IEnumerable<AppUser> GetAllProfilesAsync();
    Task<OperationDetails> UpdateProfile(ProfileUpdateDTO model);

  }
}
