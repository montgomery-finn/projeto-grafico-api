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
    public class BorderController : Controller
    {
        private readonly IMatToBase64StringService _matToBase64StringService;
        private readonly IBase64StringToMatService _base64StringToMatService;
        public BorderController(IMatToBase64StringService matToBase64StringService,
                                    IBase64StringToMatService base64StringToMatService)
        {
            _matToBase64StringService = matToBase64StringService;
            _base64StringToMatService = base64StringToMatService;
        }

        public class BorderDTO
        {
            public string base64Image { get; set; }
            public double? treshold1 { get; set; }
            public double? treshold2 { get; set; }
        }

        [HttpPost]
        public string BilateralFilter(BorderDTO dto)
        {   
            var image = _base64StringToMatService.Execute(dto.base64Image);

            Mat bordersImage = new Mat();

            CvInvoke.Canny(image, bordersImage, dto.treshold1 ?? 50,
                dto.treshold2 ?? 150);

            var response = _matToBase64StringService.Execute(bordersImage);

            return response;
        }
    }
}
