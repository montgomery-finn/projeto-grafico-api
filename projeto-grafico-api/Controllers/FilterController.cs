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
    public class FilterController : Controller
    {
        private readonly IMatToBase64StringService _matToBase64StringService;
        private readonly IBase64StringToMatService _base64StringToMatService;
        public FilterController(IMatToBase64StringService matToBase64StringService,
                                    IBase64StringToMatService base64StringToMatService)
        {
            _matToBase64StringService = matToBase64StringService;
            _base64StringToMatService = base64StringToMatService;
        }

        public class FilterDTO
        {
            public string base64Image { get; set; }
            public int? d { get; set; }
            public double? sigmaColor { get; set; }
            public double? sigmaSpace { get; set; }
        }

        [HttpPost]
        public string Filter(FilterDTO dto)
        {   
            var image = _base64StringToMatService.Execute(dto.base64Image);

            Mat imageWithBilateralFilter = new Mat();
            CvInvoke.BilateralFilter(image, imageWithBilateralFilter, dto.d ?? 3, dto.sigmaColor ?? 30, dto.sigmaSpace ?? 30);

            var response = _matToBase64StringService.Execute(imageWithBilateralFilter);

            return response;
        }
    }
}
