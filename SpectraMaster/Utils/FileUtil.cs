using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;

namespace SpectraMaster.Utils
{
    public class FileUtil:IFileUtil
    {
        private readonly IWebHostEnvironment _env;

        public FileUtil(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> CopyToServerFileAsync(IFormFile file)
        {
            string uniqureFileName = null;
            if (file != null)
            {
                var folder = Path.Combine(_env.WebRootPath, "images");
                uniqureFileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                var filepath = Path.Combine(folder, uniqureFileName);
                await file.CopyToAsync(new FileStream(filepath, FileMode.Create));
            }

            return uniqureFileName;
        }
    }
}