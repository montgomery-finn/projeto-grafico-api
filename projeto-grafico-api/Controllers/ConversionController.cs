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
    public class ConversionController : Controller
    {
        private readonly IMatToBase64StringService _matToBase64StringService;
        private readonly IBase64StringToMatService _base64StringToMatService;
        public ConversionController(IMatToBase64StringService matToBase64StringService,
                                    IBase64StringToMatService base64StringToMatService)
        {
            _matToBase64StringService = matToBase64StringService;
            _base64StringToMatService = base64StringToMatService;
        }

        public class ConvertDTO
        {
            public string base64Image { get; set; }
            public string colorModel { get; set; }
        }

        [HttpPost]
        public string Convert(ConvertDTO dto)
        {
            var colorModel = (ColorModel)Enum.Parse(typeof(ColorModel), dto.colorModel);

            var conversion = new ColorModelToColorConversionConverter()
                                    .Convert(colorModel);

            var image = _base64StringToMatService.Execute(dto.base64Image);

            Mat converted = new Mat();
            if (conversion == null)
            {
                image.CopyTo(converted);
            }
            else
            {
                CvInvoke.CvtColor(image, converted, conversion.Value);
            }

            var response = _matToBase64StringService.Execute(converted);

            return response;
        }
    }
}
