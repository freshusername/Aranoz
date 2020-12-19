using ApplicationCore.DTOs;
using ApplicationCore.DTOs.AppProfile;
using HotelsBooking.Models;
using HotelsBooking.Models.AppProfile;
using Infrastructure.Entities;
using AutoMapper;

namespace HotelsBooking.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, EditUserViewModel>();
            CreateMap<AppUser, ChangePasswordViewModel>();
            CreateMap<EditUserViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); ;
            CreateMap<ChangePasswordViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); ;

            CreateMap<AppUser, AdminUserDTO>();
            CreateMap<AdminUserDTO, UsersViewModel>();

            CreateMap<AdditionalConvDTO, AdditionalConv>().ReverseMap();

            CreateMap<HotelConvDTO, HotelConvsViewModel>();

            CreateMap<CreateOrEditHotelViewModel, HotelDTO>().ForMember(x => x.HotelPhotos, opt => opt.Ignore());
            CreateMap<HotelDTO, CreateOrEditHotelViewModel>().ForMember(x => x.HotelPhotos, opt => opt.Ignore());

            CreateMap<HotelDTO, Hotel>().ReverseMap();


            CreateMap<RegisterViewModel, UserDTO>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<UserDTO, AppUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)).ReverseMap();

            CreateMap<LoginViewModel, UserDTO>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<OrderDetail, AdminOrderDetailDTO>().ReverseMap();
            CreateMap<Order, AdminOrderDTO>().ReverseMap();


            #region Profile
            CreateMap<AppUser, ProfileDto>().ReverseMap();

            CreateMap<ProfileDto, AllProfilesViewModel>().ReverseMap();
            CreateMap<ProfileDto, ProfileViewModel>().ReverseMap();

            CreateMap<ProfileRoleDto, AppUser>().ReverseMap();

            #endregion


            CreateMap<AdminOrderDTO, OrdersViewModel>();
            CreateMap<AdminOrderDetailDTO, OrderDetailsViewModel>();

            CreateMap<CreateOrEditOrderViewModel, AdminOrderDTO>().ReverseMap();
            CreateMap<CreateOrEditOrderDetailsViewModel, AdminOrderDetailDTO>().ReverseMap();

            CreateMap<AdditionalConv, AdditionalConvDTO>().ReverseMap();
            CreateMap<ConvsViewModel, AdditionalConvDTO>().ReverseMap();

            CreateMap<CreateAndEditHotelConvViewModel, HotelConvDTO>().ReverseMap();
            CreateMap<CreateOrEditHotelConvViewModel, HotelConvDTO>().ReverseMap();

            CreateMap<HotelConv, HotelConvDTO>()
                .ForMember(hcd => hcd.Name, map => map.MapFrom(hc => hc.AdditionalConv.Name)).ReverseMap();
            CreateMap<AdditionalConv, AdditionalConvDTO>().ReverseMap();

            CreateMap<HotelRoomDTO, HotelRoomsViewModel>().ReverseMap();
            CreateMap<CreateOrEditHotelRoomViewModel, HotelRoomDTO>().ReverseMap();
            CreateMap<HotelRoom, HotelRoomDTO>().ForMember(hrd => hrd.Type, map => map.MapFrom(hr => hr.Room.RoomType)).ReverseMap();

            CreateMap<HotelRoomConvDTO, HotelRoomConvsViewModel>();
            CreateMap<Room, AdminRoomDTO>().ReverseMap();
            CreateMap<AdminRoomsViewModel, AdminRoomDTO>().ReverseMap();
        }
    }
}

