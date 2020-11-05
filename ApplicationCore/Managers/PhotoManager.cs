using ApplicationCore.Interfaces;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class PhotoManager : IPhotoManager
    {
        private readonly ApplicationDbContext db;

        public PhotoManager(
            ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<byte[]> GetPhotoFromFile(IFormFile uploadedFile, int width, int height)
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

            Image img = await ResizeImage(uploadedFile, width, height);

            imgData = ImageToByteArray(img);

            return imgData;
        }


        public async Task Delete(string id)
        {
            var photo = db.HotelPhotos.Find(id);
            if (photo != null)
            {
                db.HotelPhotos.Remove(photo);
                await db.SaveChangesAsync();
            }
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }


        private static bool IsValidImage(IFormFile file) => (file != null);

        public async Task<Image> ResizeImage(IFormFile file, int width, int height)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                using (var img = Image.FromStream(memoryStream))
                {
                    return Resize(img, width, height);
                }
            }
        }

        public Image Resize(Image image, int width, int height)
        {
            var res = new Bitmap(width, height);
            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, width, height);
            }
            return res;
        }

    }
}
