using Emgu.CV;
using Microsoft.AspNetCore.Hosting;
using projeto_grafico_api.Services.Interfaces;
using System;
using System.IO;

namespace projeto_grafico_api.Services
{
    public class MatToBase64StringService : IMatToBase64StringService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MatToBase64StringService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Execute(Mat mat)
        {
            var contentPath = _webHostEnvironment.ContentRootPath;
            var guid = Guid.NewGuid().ToString();
            var filePath = Path.Combine(contentPath, guid) + ".jpeg";

            CvInvoke.Imwrite(filePath, mat);

            var fileBytes = File.ReadAllBytes(filePath);

            return Convert.ToBase64String(fileBytes);
        }
    }
}
