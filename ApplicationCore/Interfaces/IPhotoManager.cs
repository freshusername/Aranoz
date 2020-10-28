using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    interface IPhotoManager
    {
        Task<HotelPhoto> AddPhoto(IFormFile uploadedFile);
        Task Delete(Guid id);
    }
}
