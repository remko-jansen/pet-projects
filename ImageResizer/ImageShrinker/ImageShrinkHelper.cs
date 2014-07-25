using System;
using System.Drawing;
using System.IO;
using ImageResizer;

namespace ImageShrinker
{
    public class ImageShrinkHelper
    {
        private string _imageFilePath;
        private int _requestedSize;

        private int _width;
        private int _height;

        public ImageShrinkHelper(string imageFilePath, int requestedSize)
        {
            _imageFilePath = imageFilePath;
            _requestedSize = requestedSize;
        }

        public void DoShrink()
        {
            ExtractImageInfo();

            if (CanShrink())
            {
                var bmap = GetBitmap();
            }


        }

        private Bitmap GetBitmap()
        {
            var outStream = new MemoryStream();

            ImageBuilder.Current.Build(_imageFilePath, outStream, new ResizeSettings());

            outStream.Position = 0;
            var bitmap = new Bitmap(outStream);
            return bitmap;
        }

        private void ExtractImageInfo()
        {
            var info = ImageBuilder.Current.LoadImageInfo(_imageFilePath, null);

            _width = Convert.ToInt32(info["source.width"]);
            _height = Convert.ToInt32(info["source.height"]);
        }

        private bool CanShrink()
        {
            return _width > _requestedSize || _height > _requestedSize;
        }
    }
}