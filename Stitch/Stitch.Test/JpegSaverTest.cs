using SharpTestsEx;
using Stitch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing.Imaging;

namespace Stitch.Test
{
    [TestClass]
    public class JpegSaverTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        public void GetEncoder_Should_Get_Jpeg_Encoder()
        {
            // Arrange
            var target = new JpegSaver();

            // Act
            var result = target.GetEncoder();

            // Assert
            result.FormatID.Should().Be(ImageFormat.Jpeg.Guid);
        }

        [TestMethod]
        public void GetFileExtensionTest()
        {
            // Arrange
            var target = new JpegSaver();

            // Act
            var result = target.GetFileExtension();

            // Assert
            result.Should().Be("*.JPG");
        }
    }
}
