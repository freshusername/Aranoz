using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly IPhotoManager photoManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ProfileManager(IPhotoManager _photoManager, ApplicationDbContext _context, IMapper _mapper)
        {
            photoManager = _photoManager;
            context = _context;
            mapper = _mapper;
        }
        public async Task<IActionResult> DetailAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DetailAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> UploadProfileImageAsync(IFormFile image)
        {

            var newimage = await photoManager.GetPhotoFromFile(image, 450, 450);

            return newimage;
        }

        public async Task<OperationDetails> UpdateProfileInfoAsync(ProfileUpdateDTO profileDTO)
        {
            AppUser user = context.Users.FirstOrDefault(u => u.Email == profileDTO.Email);
            user.ProfileImage = await UploadProfileImageAsync(profileDTO.ProfileImage);
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return new OperationDetails(true, "User update", "Name");
        }
    }
}
