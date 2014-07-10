using SharpTestsEx;
using Stitch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Stitch.Test
{
    [TestClass]
    public class ImageStitcherTest
    {
        private TestContext testContextInstance;

        private ImageStitcher _stitcher;

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
        //[ClassInitialize]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup]
        //public static void MyClassCleanup()
        //{
        //}
        
        [TestInitialize]
        public void MyTestInitialize()
        {
            _stitcher = new ImageStitcher();
        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        public void GetOutputFileName_Should_Return_Null_When_File1_And_File2_Are_Null()
        {
            // Act
            var result =_stitcher.GetOutputFileName(null, null);

            // Assert
            result.Should().Be.Null();
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_Empty_String_When_File_Path_Is_Invalid()
        {
            const string filePath1 = "\\:test\\image.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, null);

            // Assert
            result.Should().Be.Empty();
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_Name_Based_On_File1_When_File2_Is_Null()
        {
            const string filePath1 = "\\test\\image.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, null);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().Not.Be(filePath1);

            result.Should().StartWith(Path.GetDirectoryName(filePath1));
            result.Should().EndWith("\\test\\image_Stitched.jpg");
        }
        
        [TestMethod]
        public void GetOutputFileName_Should_Return_Name_Based_On_File1_When_File2_Is_Empty()
        {
            const string filePath1 = "\\test\\image.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, "");

            // Assert
            result.Should().Not.Be.Null();
            result.Should().Not.Be(filePath1);

            result.Should().StartWith(Path.GetDirectoryName(filePath1));
            result.Should().EndWith("\\test\\image_Stitched.jpg");
        }
        
        [TestMethod]
        public void GetOutputFileName_Should_Return_Name_Based_On_File2_When_File1_Is_Null()
        {
            const string filePath2 = "\\test\\image.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(null, filePath2);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().Not.Be(filePath2);

            result.Should().StartWith(Path.GetDirectoryName(filePath2));
            result.Should().EndWith("\\test\\image_Stitched.jpg");
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_Name_Based_On_File2_When_File1_Is_Empty()
        {
            const string filePath2 = "\\test\\image.jpg";

            // Act
            var result = _stitcher.GetOutputFileName("", filePath2);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().Not.Be(filePath2);

            result.Should().StartWith(Path.GetDirectoryName(filePath2));
            result.Should().EndWith("\\test\\image_Stitched.jpg");
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_A_Name_Based_On_The_Common_Part_Of_The_Name_Of_File1_And_File2_When_Provided()
        {
            const string filePath1 = "\\test\\panorama1.jpg";
            const string filePath2 = "\\test\\panorama2.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, filePath2);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().StartWith(Path.GetDirectoryName(filePath1));
            result.Should().EndWith("\\test\\panorama.jpg");
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_A_Name_Based_On_The_Common_Part_Of_The_Name_Of_File1_And_File2_When_Provided_But_Without_Spaces()
        {
            const string filePath1 = "\\test\\panorama 1.jpg";
            const string filePath2 = "\\test\\panorama 2.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, filePath2);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().StartWith(Path.GetDirectoryName(filePath1));
            result.Should().EndWith("\\test\\panorama.jpg");
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_A_Name_Based_On_File1_When_The_Name_Of_File1_And_File2_Dont_Have_A_Common_Part()
        {
            const string filePath1 = "\\test\\panorama huis.jpg";
            const string filePath2 = "\\test\\boom.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, filePath2);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().StartWith(Path.GetDirectoryName(filePath1));
            result.Should().EndWith("\\test\\panorama huis_Stitched.jpg");
        }

        [TestMethod]
        public void GetOutputFileName_Should_Return_A_Name_Based_On_File1_When_The_Common_Part_Of_File1_And_File2_Equals_The_Name_Of_File1()
        {
            const string filePath1 = "\\test\\panorama huis.jpg";
            const string filePath2 = "\\test\\panorama huis2.jpg";

            // Act
            var result = _stitcher.GetOutputFileName(filePath1, filePath2);

            // Assert
            result.Should().Not.Be.Null();
            result.Should().StartWith(Path.GetDirectoryName(filePath1));
            result.Should().EndWith("\\test\\panorama huis_Stitched.jpg");
        }
    }
}
