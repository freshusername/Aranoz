using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHotelManager : IDisposable
    {
        List<Hotel> GetHotels();
        Task<OperationDetails> Create(HotelDTO hotelDTO);
        Task Delete(int Id);
    }
}
