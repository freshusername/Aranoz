using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Managers
{
  public class ProfileManager : IProfileManager
  {
    public async Task<IActionResult> DetailAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<IActionResult> DetailAsync(string id)
    {
      throw new NotImplementedException();
    }

    public async Task<IActionResult> UploadProfileImageAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<IActionResult> UpdateProfileInfoAsync()
    {
      throw new NotImplementedException();
    }
  }
}
