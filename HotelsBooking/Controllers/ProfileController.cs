using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Services;
using AutoMapper;
using HotelsBooking.Models;
using Infrastructure.Entities;
using HotelsBooking.Models.AppProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;

namespace HotelsBooking.Controllers
{
  public class ProfileController : Controller
  {
    private readonly IMapper _mapper;
    private readonly IProfileService _profileService;

    public ProfileController(IMapper mapper, IProfileService profileService)
    {
      _mapper = mapper;
      _profileService = profileService;
    }

    public async Task<IActionResult> Detail(string id)
    {
      var profile = await _profileService.GetByIdAsync(id);
      var result = _mapper.Map<ProfileDto, ProfileViewModel>(profile);
      return View(result);
    }

    public async Task<IActionResult> Index()
    {
      var profiles =  _profileService
        .GetAllProfilesAsync()
        .Select(pr => new ProfileViewModel
        {
          ProfileId = pr.Id,
          //Role = Roles
          Email = pr.Email,
          FirstName = pr.FirstName,
          LastName = pr.LastName
        });
      
      //var result = _mapper.Map<IEnumerable<ProfileDto>, IEnumerable<AllProfilesViewModel>>(profiles);
      var model = new AllProfilesViewModel
      {
        profilesList = profiles
      };

      return View(model);
    }

    public async Task<IActionResult> Edit(string id)
    {
      var profile = await _profileService.GetByIdAsync(id);

      return View(new ProfileUpdateViewModel
      {
        FirstName = profile.FirstName,
        LastName = profile.LastName,
        Email = profile.Email
      });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(ProfileDto model)
    {
      var profile = await _profileService.GetByEmailAsync(model.Email);
      await _profileService.UpdateProfile(model);

      return RedirectToAction("Detail", "Profile", new {id = profile.Id});

    }
  }
}
