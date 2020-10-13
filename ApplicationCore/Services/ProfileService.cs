using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApplicationCore.Services
{
  public class ProfileService : IProfileService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public ProfileService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ProfileDTO> GetByIdAsync(string id)
    {
      var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
      if (user == null)
        return null;

      var profile = _mapper.Map<AppUser, ProfileDTO>(user);
      return profile;
    }

    public IEnumerable<AppUser> GetAllProfilesAsync()
    {
      var users = _context.Users;
      //var profiles = _mapper.Map<IEnumerable<AppUser>, IEnumerable<ProfileDTO>>(users);
      return users; 
    }
  }
}
