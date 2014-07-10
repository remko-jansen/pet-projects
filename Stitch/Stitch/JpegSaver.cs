using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Stitch
{
    public class JpegSaver : IImageSaver
    {
        private readonly ImageFormat _format;

        public JpegSaver()
        {
            _format = ImageFormat.Jpeg;
            ImageQuality = 75;
        }

        public long ImageQuality { get; set; }

        public void Save(Image outputImage, FileInfo outputFile)
        {
            outputImage.Save(outputFile.FullName, GetEncoder(), GetEncoderParameters());
        }

        public string GetFileExtension()
        {
            ImageCodecInfo encoder = GetEncoder();
            var extension = encoder.FilenameExtension.Split(';').First();

            return extension;
        }

        public ImageCodecInfo GetEncoder()
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            return codecs.FirstOrDefault(codec => codec.FormatID == _format.Guid);
        }

        public EncoderParameters GetEncoderParameters()
        {
            var jpegParameters = new EncoderParameters(1);
            jpegParameters.Param[0] = new EncoderParameter(Encoder.Quality, ImageQuality);
            return jpegParameters;
        }

    }
}
