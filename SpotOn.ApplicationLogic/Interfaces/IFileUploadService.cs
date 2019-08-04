using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Interfaces
{
    public interface IFileUploadService
    {
        string UploadFile(IFormFile file, string folder);
    }
}
