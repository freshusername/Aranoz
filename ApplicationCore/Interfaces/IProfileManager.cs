using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces
{
    public interface IProfileManager
    {
        Task<IActionResult> DetailAsync(string id); 
        Task<byte[]> UploadProfileImageAsync(IFormFile image);
        Task<OperationDetails> UpdateProfileInfoAsync(ProfileUpdateDTO profileDTO);
    }
}
