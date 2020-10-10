using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    //private readonly IApplicationUser _userService;
    //private readonly IUpload _uploadService;
    //private readonly IConfiguration _configuration;

    public ProfileController(
      UserManager<AppUser> userManager
      //IApplicationUser userService, 
      //IUpload uploadService, 
      //IConfiguration configuration
      )
    {
      _userManager = userManager;
      //_userService = userService;
      //_uploadService = uploadService;
      //_configuration = configuration;
    }

  //  [Authorize]
  //  public async Task<IActionResult> Detail(string id)
  //  {
  //    var user = await _userManager.FindByIdAsync(id);
  //    var userRoles = _userManager.GetRolesAsync(user).Result;

  //    var model = new ProfileModel()
  //    {
  //      UserId = user.Id,
  //      Username = user.UserName,
  //      UserRating = user.Rating.ToString(),
  //      Email = user.Email,
  //      ProfileImageUrl = user.ProfileImageUrl,
  //      DateJoined = user.MemberSince,
  //      IsActive = user.IsActive,
  //      IsAdmin = userRoles.Contains("Admin")
  //    };

  //    return View(model);
  //  }

  //  /*
  //   * Files uploaded using the IFormFile technique are buffered in memory or on disk on the web server 
  //   * before being processed. Inside the action method, the IFormFile contents are accessible as a stream. 
  //   * In addition to the local file system, files can be streamed to Azure Blob storage or Entity Framework.
  //   */

  //  [HttpPost]
  //  public async Task<IActionResult> UploadProfileImage(IFormFile file)
  //  {
  //    var userId = _userManager.GetUserId(User);
  //    var connectionString = _configuration.GetConnectionString("AzureStorageAccountConnectionString");
  //    var container = _uploadService.GetBlobContainer(connectionString);

  //    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
  //    var filename = Path.Combine(parsedContentDisposition.FileName.Trim('"'));

  //    var blockBlob = container.GetBlockBlobReference(filename);

  //    await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
  //    await _userService.SetProfileImage(userId, blockBlob.Uri);

  //    return RedirectToAction("Detail", "Profile", new { id = userId });
  //  }

  //  public IActionResult Index()
  //  {
  //    var profiles = _userService.GetAll()
  //        .OrderByDescending(user => user.Rating)
  //        .Select(u => new ProfileModel
  //        {
  //          Email = u.Email,
  //          ProfileImageUrl = u.ProfileImageUrl,
  //          UserRating = u.Rating.ToString(),
  //          DateJoined = u.MemberSince,
  //          IsActive = u.IsActive
  //        });

  //    var model = new ProfileListModel
  //    {
  //      Profiles = profiles
  //    };

  //    return View(model);
  //  }

  //  public IActionResult Deactivate(string userId)
  //  {
  //    var user = _userService.GetById(userId);
  //    _userService.Deactivate(user);
  //    return RedirectToAction("Index", "Profile");
  //  }
  }
}
