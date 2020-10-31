using ApplicationCore.DTOs;
using AutoMapper;
using HotelsBooking.Models;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,EditUserViewModel>();
            CreateMap<AppUser, ChangePasswordViewModel>();
            CreateMap<EditUserViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); ;
            CreateMap<ChangePasswordViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); ;

            CreateMap<AppUser, AdminUserDTO>();
            CreateMap<AdminUserDTO, UsersViewModel>();

            CreateMap<AdditionalConvDTO, AdditionalConv>();
            CreateMap<AdditionalConv,AdditionalConvDTO>();

            CreateMap<HotelConvDTO,HotelConvsViewModel>();

            CreateMap<CreateOrEditHotelViewModel, HotelDTO>();
            CreateMap<HotelDTO ,CreateOrEditHotelViewModel>();
            CreateMap<HotelDTO, Hotel>();
            CreateMap<Hotel,HotelDTO>();

            CreateMap<RegisterViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<UserDTO, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<LoginViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<UserDTO, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<OrderDTO, Order>();
        }
    }
}

