using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SpotOn.ApplicationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpotOn.ApplicationLogic.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileUploadService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string UploadFile(IFormFile file, string folder)
        {
            string fileName = null;
            if(file != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                fileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return fileName;
        }
    }
}
