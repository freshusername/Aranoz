using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Services;
using AutoMapper;
using HotelsBooking.Models;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBooking.Controllers
{
  public class ProfileController : Controller
  {
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IProfileService _profileService;

    public ProfileController(IMapper mapper, UserManager<AppUser> userManager, IProfileService profileService)
    {
      _mapper = mapper;
      _userManager = userManager;
      _profileService = profileService;
    }

    public async Task<IActionResult> Detail([FromRoute] string id)
    {
      var profile = await _profileService.GetByIdAsync(id);
      var result = _mapper.Map<ProfileDTO, ProfileViewModel>(profile);
      return View(result);
    }

    public async Task<IActionResult> Index()
    {
      var profiles = _profileService
        .GetAllProfilesAsync()
        .Select(pr => new ProfileViewModel
        {
          Email = pr.Email,
          FirstName = pr.FirstName,
          LastName = pr.LastName
        });
      
      //var result = _mapper.Map<IEnumerable<ProfileDTO>, IEnumerable<AllProfilesViewModel>>(profiles);
      var model = new AllProfilesViewModel
      {
        profilesList = profiles
      };

      return View(model);
    }
  }
}
