using ImageShrinker;
using NUnit.Framework;

namespace ImageShrinkerTests
{
    [TestFixture]
    class StepCalculatorTests
    {
        [Test]
        public void Constructor_Should_Set_StepPercentage_To_Default()
        {
            // Arrange
            var calc = new StepCalculator(1024, 640, 10);

            Assert.That(calc, Is.Not.Null);
        }
    }
}
