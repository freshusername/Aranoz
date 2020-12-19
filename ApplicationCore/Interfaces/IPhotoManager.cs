using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPhotoManager
    {
        Task<byte[]> GetPhotoFromFile(IFormFile uploadedFile, int width, int height);
        Task<byte[]> UploadProfileImageAsync(IFormFile image);
        Task<List<HotelPhoto>> InsertHotelPhotoAsync(List<IFormFile> images, int hotelId);
        Task Delete(string id);
    }
}
