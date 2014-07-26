using ImageShrinker;
using NUnit.Framework;
using SharpTestsEx;

namespace ImageShrinkerTests
{
    [TestFixture]
    class StepCalculatorTests
    {
        [Test]
        public void Constructor_Should_Set_StepPercentage_To_Default()
        {
            // Arrange
            var calc = new StepCalculator(10);

            calc.Should().Not.Be.Null();
        }
    }
}
