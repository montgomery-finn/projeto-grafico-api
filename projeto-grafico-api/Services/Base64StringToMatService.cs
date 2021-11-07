using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.AspNetCore.Hosting;
using projeto_grafico_api.Services.Interfaces;
using System;
using System.IO;

namespace projeto_grafico_api.Services
{
    public class Base64StringToMatService : IBase64StringToMatService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Base64StringToMatService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public Mat Execute(string base64Image)
        {
            var splittedString = base64Image.Split(";");
            var base64String = splittedString[1].Replace("base64,", "");
            var extension = splittedString[0].Split("/")[1];

            var contentPath = _webHostEnvironment.ContentRootPath;
            var fileName = Guid.NewGuid().ToString() + "." + extension;
            var filePath = Path.Combine(contentPath, fileName);

            File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));

            Mat mat = CvInvoke.Imread(filePath, ImreadModes.AnyColor);

            File.Delete(filePath);

            return mat;
        }
    }
}
