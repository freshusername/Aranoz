using ApplicationCore.Interfaces;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    class PhotoManager : IPhotoManager
    {
        private readonly ApplicationDbContext db;

        public PhotoManager(
            ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<HotelPhoto> AddPhoto(IFormFile uploadedFile)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            byte[] imgData;
            using (var reader = new BinaryReader(uploadedFile.OpenReadStream()))
            {
                imgData = reader.ReadBytes((int)uploadedFile.Length);
            }

            var photo = new HotelPhoto
            {
                Image = imgData,
            };

            db.HotelPhotos.Add(photo);
            await db.SaveChangesAsync();

            return photo;
        }


        public async Task Delete(Guid id)
        {
            var photo = db.HotelPhotos.Find(id);
            if (photo != null)
            {
                db.HotelPhotos.Remove(photo);
                await db.SaveChangesAsync();
            }
        }


        private static bool IsValidImage(IFormFile file) => (file != null);

    }
}
