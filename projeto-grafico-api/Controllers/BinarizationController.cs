using Emgu.CV;
using Microsoft.AspNetCore.Mvc;
using projeto_grafico_api.Enums;
using projeto_grafico_api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto_grafico_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BinarizationController : Controller
    {
        private readonly IMatToBase64StringService _matToBase64StringService;
        private readonly IBase64StringToMatService _base64StringToMatService;
        public BinarizationController(IMatToBase64StringService matToBase64StringService,
                                    IBase64StringToMatService base64StringToMatService)
        {
            _matToBase64StringService = matToBase64StringService;
            _base64StringToMatService = base64StringToMatService;
        }

        public class BinarizationDTO
        {
            public string base64Image { get; set; }
            public string threshold { get; set; }
        }

        [HttpPost]
        public string Binarize(BinarizationDTO dto)
        {   
            var image = _base64StringToMatService.Execute(dto.base64Image);

            Mat grayScaleImage = new Mat();
            CvInvoke.CvtColor(image, grayScaleImage, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            var thresholdValue = String.IsNullOrEmpty(dto.threshold) ? 0 : Convert.ToInt32(dto.threshold);
            Mat thresholdImage = new Mat();
            if (thresholdValue == 0)
            {
                CvInvoke.Threshold(grayScaleImage, thresholdImage, 0, 255, Emgu.CV.CvEnum.ThresholdType.Binary | Emgu.CV.CvEnum.ThresholdType.Otsu);
            }
            else
            {
                CvInvoke.Threshold(grayScaleImage, thresholdImage, thresholdValue, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            }

            var response = _matToBase64StringService.Execute(thresholdImage);

            return response;
        }
    }
}
