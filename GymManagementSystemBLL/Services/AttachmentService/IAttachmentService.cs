using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? Upload(string folderName, IFormFile file);

        bool Delete(string folderName, string fileName);
    }
}
