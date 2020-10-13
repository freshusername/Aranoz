﻿using ApplicationCore.DTOs;
using AutoMapper;
using HotelsBooking.Models;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Mapping
{

    namespace CoffeStoreWeb.AutoMapper

    {

        public class AutoMapperProfile : Profile

        {

            public AutoMapperProfile()

            {

                CreateMap<RegisterViewModel, UserDTO>()
                  .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
                CreateMap<UserDTO, AppUser>()
                  .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

                CreateMap<LoginViewModel, UserDTO>()
                  .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
                CreateMap<UserDTO, AppUser>()
                  .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

                CreateMap<AppUser, ProfileDTO>().ReverseMap();
                CreateMap<ProfileDTO, AllProfilesViewModel>().ReverseMap();
                CreateMap<ProfileDTO, ProfileViewModel>().ReverseMap();

      }

        }

    }
}
