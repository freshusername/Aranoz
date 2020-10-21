using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using ApplicationCore.DTOs;

namespace HotelsBooking.Mapping
{
    public class ApplicationMappingProfile : AutoMapper.Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}
