using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces
{
  public interface IProfileManager
  {
    Task<IActionResult> DetailAsync(string id); 
    Task<IActionResult> UploadProfileImageAsync();
  }
}
