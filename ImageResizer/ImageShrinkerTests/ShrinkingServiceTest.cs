using System;
using System.Windows.Media.Imaging;
using ImageShrinker;
using NUnit.Framework;
using SharpTestsEx;
using System.Drawing;
using System.Linq;

namespace ImageShrinkerTests
{
    [TestFixture]
    public class ShrinkingServiceTest
    {
        [Test]
        public void Test_Get_File_Extension_From_Encoder()
        {
            // Arrange
            var encoder = (BitmapEncoder)Activator.CreateInstance(typeof(JpegBitmapEncoder));

            var s = encoder.CodecInfo.FileExtensions;

        }

    }
}