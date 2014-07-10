using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Stitch
{
    interface IImageSaver
    {
        void Save(Image outputImage, FileInfo outputFile);
        string GetFileExtension();
        ImageCodecInfo GetEncoder();
        EncoderParameters GetEncoderParameters();
    }
}
