using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto_grafico_api.Services.Interfaces
{
    public interface IBase64StringToMatService
    {
        public Mat Execute(string base64Image);
    }
}
