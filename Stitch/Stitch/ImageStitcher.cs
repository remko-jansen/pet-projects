using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Stitch
{
    public class ImageStitcher
    {
        private readonly FileInfo _imageFile1;
        private readonly FileInfo _imageFile2;
        private IBorder _border;

        private Image Image1 { get; set; }
        private Image Image2 { get; set; }
        private Bitmap OutputImage { get; set; }

        public IBorder Border
        {
            get
            {
                if (_border == null)
                    _border = new NoBorder();
                return _border;
            }
            set { _border = value; }
        }

        public ImageStitcher(FileInfo imageFile1, FileInfo imageFile2)
        {
            _imageFile1 = imageFile1;
            _imageFile2 = imageFile2;
        }

        public Image DoStitch()
        {
            Image1 = Image.FromFile(_imageFile1.FullName);
            Image2 = Image.FromFile(_imageFile2.FullName);

            var outputWidth = OutputWidth();
            var outputHeight = OutputHeight();

            OutputImage = new Bitmap(outputWidth, outputHeight, PixelFormat.Format24bppRgb);
            OutputImage.SetResolution(Image1.HorizontalResolution, Image1.VerticalResolution);

            var graphics = Graphics.FromImage(OutputImage);

            var positionImage1 = new Rectangle(0, 0, Image1.Width, Image1.Height);
            var positionImage2 = new Rectangle(Image1.Width, 0, Image2.Width, Image2.Height);

            if (!Border.Inside)
            {
                positionImage1.Offset(Border.ThicknessLeft, Border.ThicknessTop);
                positionImage2.Offset(2 * Border.ThicknessLeft, Border.ThicknessTop);
            }

            graphics.DrawImage(Image1, positionImage1);
            graphics.DrawImage(Image2, positionImage2);

            Border.Draw(graphics, positionImage1);
            Border.Draw(graphics, positionImage2);

            return OutputImage;
        }

        private int OutputWidth()
        {
            int width = Image1.Width + Image2.Width;

            if (!Border.Inside)
            {
                width += Border.ThicknessLeft * 2  + Border.ThicknessRight;
            }
            return width;
        }

        private int OutputHeight()
        {
            var height1 = Image1.Height;
            var height2 = Image2.Height;

            if (!Border.Inside)
            {
                height1 += Border.ThicknessTop + Border.ThicknessBottom;
                height2 += Border.ThicknessTop + Border.ThicknessBottom;
            }

            return Math.Max(height1, height2);
        }
    }
}