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
    public class MorphologyController : Controller
    {
        private readonly IMatToBase64StringService _matToBase64StringService;
        private readonly IBase64StringToMatService _base64StringToMatService;
        public MorphologyController(IMatToBase64StringService matToBase64StringService,
                                    IBase64StringToMatService base64StringToMatService)
        {
            _matToBase64StringService = matToBase64StringService;
            _base64StringToMatService = base64StringToMatService;
        }

        public enum EMorphology
        {
            Open,
            Close
        }

        public class MorphologyDTO
        {
            public string base64Image { get; set; }
            public string morphology { get; set; }
        }

        [HttpPost]
        public string BilateralFilter(MorphologyDTO dto)
        {   
            var image = _base64StringToMatService.Execute(dto.base64Image);

            var morphology = (EMorphology)Convert.ToInt32(dto.morphology);

            Mat morphologyImage = new Mat();

            var structuringElement = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new System.Drawing.Size(3,3), new System.Drawing.Point(-1, -1));
           
            CvInvoke.MorphologyEx(image, morphologyImage, morphology == EMorphology.Open ? Emgu.CV.CvEnum.MorphOp.Open : Emgu.CV.CvEnum.MorphOp.Close,
                structuringElement, new System.Drawing.Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);

            var response = _matToBase64StringService.Execute(morphologyImage);

            return response;
        }
    }
}
