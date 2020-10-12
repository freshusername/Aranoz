using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApplicationCore.Services
{
    public class ProfileService : IProfileService
    {
        private ApplicationDbContext _context;
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;



        public ProfileService(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ProfileDTO> GetByIdAsync(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return null;

            var profile = _mapper.Map<AppUser, ProfileDTO>(user);
            return profile;
        }

        public async Task<ProfileDTO> GetByEmailAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;

            var profile = _mapper.Map<AppUser, ProfileDTO>(user);
            return profile;
        }

        public async Task<OperationDetails> UpdateProfile(ProfileUpdateDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new OperationDetails(false, "Something gone wrong", "Email");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            await _context.SaveChangesAsync();

            return new OperationDetails(true, "Your profile has been successfully updated", "Email");
        }

        public IEnumerable<AppUser> GetAllProfilesAsync()
        {
            var users = _context.Users;
            //var profiles = _mapper.Map<IEnumerable<AppUser>, IEnumerable<ProfileDTO>>(users);
            return users;
        }

        //public Task<Profile> UpdateProfile()
        //{

        //}
    }
}
