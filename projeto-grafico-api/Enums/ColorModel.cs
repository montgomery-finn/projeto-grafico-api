
using Emgu.CV.CvEnum;

namespace projeto_grafico_api.Enums
{
    public enum ColorModel
    {
        RGB,
        HSV,
        XYZ,
        HLS,
        YCrCb,
        Lab,
        Luv
    }

    public class ColorModelToColorConversionConverter
    {
        public ColorConversion? Convert(ColorModel colorModel)
        {
            switch (colorModel)
            {
                case ColorModel.HSV:
                    return ColorConversion.Bgr2Hsv;
                case ColorModel.XYZ:
                    return ColorConversion.Bgr2Xyz;
                case ColorModel.HLS:
                    return ColorConversion.Bgr2Hls;
                case ColorModel.YCrCb:
                    return ColorConversion.Bgr2YCrCb;
                case ColorModel.Lab:
                    return ColorConversion.Bgr2Lab;
                case ColorModel.Luv:
                    return ColorConversion.Bgr2Luv;
                default:
                    return null;
            }
        }
    }
}
