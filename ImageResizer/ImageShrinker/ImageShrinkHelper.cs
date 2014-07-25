using System.Drawing;
using ImageResizer;

namespace ImageShrinker
{
    public class ImageShrinkHelper
    {
        private string _imageFilePath;
        private int _requestedSize;

        public ImageShrinkHelper(string imageFilePath, int requestedSize)
        {
            _imageFilePath = imageFilePath;
            _requestedSize = requestedSize;
        }

        public void DoShrink()
        {
            var bmap = GetBitmap();

        }

        private Bitmap GetBitmap()
        {
            var info = ImageBuilder.Current.LoadImageInfo(_imageFilePath, null);
            return null;
        }
    }
}