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
        Task Delete(string id);
    }
}
