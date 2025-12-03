
using Microsoft.AspNetCore.Hosting;

namespace Ecommerce_WatchShop.Helper
{
    public class UploadImageHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadImageHelper(IWebHostEnvironment environment)
        {
            _webHostEnvironment = environment;
        }

        public async Task<string?> UploadImage(IFormFile? file, string slug)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");

            if (file == null || !file.ContentType.StartsWith("image/"))
                return null;

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{slug}{fileExtension}";
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public string ChangeFileName(string oldImg, string newSlug)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");

            var oldImagePath = Path.Combine(uploadsFolder, oldImg);

            string fileExtension = Path.GetExtension(oldImagePath);
            string fileName = $"{newSlug}{fileExtension}";
            string newImagePath = Path.Combine(uploadsFolder, fileName);

            if (oldImg != fileName)
            {
                System.IO.File.Move(oldImagePath, newImagePath);
            }

            return fileName;
        }

        public void DeleteImage(string fileName)
        {
            try
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");
                var filePath = Path.Combine(uploadsFolder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                // Ignore errors when deleting files
            }
        }

        // Static methods for banner management
        public static async Task<string?> UploadImageAsync(IFormFile? file, string folderName)
        {
            if (file == null || !file.ContentType.StartsWith("image/"))
                return null;

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public static void DeleteImage(string fileName, string folderName)
        {
            try
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                // Ignore errors when deleting files
            }
        }
    }
}
