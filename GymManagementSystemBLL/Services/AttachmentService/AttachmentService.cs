using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] allowedExtentions = {".jpg", ".jpeg", ".png"};
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5MB
        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                // (1) Check Extention
                var extention = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtentions.Contains(extention)) return null;

                // (2) Check Size
                if (folderName is null || file is null || file.Length == 0) return null;
                if (file.Length > maxFileSize) return null;

                // (3) Get Located Folder Path
                var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", folderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                // (4) Make File Name Unique - GUID
                //var UniqueFileName = Guid.NewGuid().ToString() + extention;
                // Using String Interpolation => Calling ToString() Directly
                var uniqueFileName = $"{Guid.NewGuid()}{extention}";

                // (5) Get Fill Path
                var filePath = Path.Combine(FolderPath, uniqueFileName);

                // (6) Create File Stream to Copy File
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // (7) Use Stream to Copy File
                    file.CopyTo(fileStream);
                }
                ;

                // (8) Return FileName To Store in Database
                return uniqueFileName;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Faild to upload photo : {ex}");
                return null;
            }
        }
        public bool Delete(string folderName, string fileName)
        {
            try
            {
                //(1) Get Located File Path
                if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName)) return false;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", folderName, fileName);

                //(2) If File Exists, Delete It
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Faild to delete photo : {ex}");
                return false;
            }
        }
    }
}
