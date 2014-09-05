using System;
using System.Drawing;
using System.IO;

namespace ImageShrinker
{
    public class ImageShrinkBatcher
    {
        private string _imageFilePath;
        private int _requestedSize;

        public ImageShrinkBatcher(string imageFilePath, int requestedSize)
        {
            _imageFilePath = imageFilePath;
            _requestedSize = requestedSize;
        }

        public void DoShrink()
        {
            var service = new ShrinkingService();

            service.Shrink(_imageFilePath, _requestedSize);
        }

    }
}