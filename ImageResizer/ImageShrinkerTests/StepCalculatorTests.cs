using System.Drawing;
using System.Security.Cryptography;
using ImageShrinker;
using NUnit.Framework;
using SharpTestsEx;

namespace ImageShrinkerTests
{
    [TestFixture]
    class StepCalculatorTests
    {
        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Width_Is_Largest()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(3);
            result[0].Should().Be(new Size(1024, 768));
            result[1].Should().Be(new Size(768, 576));
            result[2].Should().Be(new Size(576, 432));
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Heigth_Is_Largest()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(768, 1024);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(3);
            result[0].Should().Be(new Size(768, 1024));
            result[1].Should().Be(new Size(576, 768));
            result[2].Should().Be(new Size(432, 576));
        }
    }
}
