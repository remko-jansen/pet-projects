using ImageShrinker;
using NUnit.Framework;
using SharpTestsEx;
using System.Drawing;
using System.Linq;

namespace ImageShrinkerTests
{
    [TestFixture]
    class StepCalculatorTests
    {
        [Test]
        public void GetSteps_Should_Return_Start_Size_As_The_First_Result()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(2048, 1536);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be.GreaterThan(1);
            result.First().Should().Be(start);
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps()
        {
            // Arrange
            var calc = new StepCalculator(20);

            var start = new Size(5000, 3750);

            // Act
            var result = calc.GetSteps(start, 2048);

            // Assert
            result.Count.Should().Be(5);
            result[0].Should().Be(new Size(5000, 3750));
            result[1].Should().Be(new Size(4000, 3000));
            result[2].Should().Be(new Size(3200, 2400));
            result[3].Should().Be(new Size(2560, 1920));
            result[4].Should().Be(new Size(2048, 1536));
        }

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
            result[2].Should().Be(new Size(640, 480));
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
            result[2].Should().Be(new Size(480, 640));
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Width_Is_Largest_And_Only_1_Step_Is_Needed()
        {
            // Arrange
            var calc = new StepCalculator(75);

            var start = new Size(1280, 1024);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(2);
            result[0].Should().Be(new Size(1280, 1024));
            result[1].Should().Be(new Size(640, 512));
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Heigth_Is_Largest_And_Only_1_Step_Is_Needed()
        {
            // Arrange
            var calc = new StepCalculator(75);

            var start = new Size(1024, 1280);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(2);
            result[0].Should().Be(new Size(1024, 1280));
            result[1].Should().Be(new Size(512, 640));
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Heigth_Equals_Width()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(1024, 1024);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(3);
            result[0].Should().Be(new Size(1024, 1024));
            result[1].Should().Be(new Size(768, 768));
            result[2].Should().Be(new Size(640, 640));
        }

        [Test]
        public void GetSteps_Should_Return_Only_Start_Size_When_Width_Equals_Goal()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 1024);

            // Assert
            result.Count.Should().Be(1);
            result[0].Should().Be(new Size(1024, 768));
        }

        [Test]
        public void GetSteps_Should_Return_Only_Start_Size_When_Height_Equals_Goal()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(768, 1024);

            // Act
            var result = calc.GetSteps(start, 1024);

            // Assert
            result.Count.Should().Be(1);
            result[0].Should().Be(new Size(768, 1024));
        }

        [Test]
        public void GetSteps_Should_Return_Only_Start_Size_When_Width_Is_Smaller_Than_Goal()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 1025);

            // Assert
            result.Count.Should().Be(1);
            result[0].Should().Be(new Size(1024, 768));
        }

        [Test]
        public void GetSteps_Should_Return_Only_Start_Size_When_Height_Is_Smaller_Than_Goal()
        {
            // Arrange
            var calc = new StepCalculator(25);

            var start = new Size(768, 1024);

            // Act
            var result = calc.GetSteps(start, 1025);

            // Assert
            result.Count.Should().Be(1);
            result[0].Should().Be(new Size(768, 1024));
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Width_And_Height_Differs_Greatly()
        {
            // Arrange
            var calc = new StepCalculator(20);

            var start = new Size(3400, 100);

            // Act
            var result = calc.GetSteps(start, 2048);

            // Assert
            result.Count.Should().Be(4);
            result[0].Should().Be(new Size(3400, 100));
            result[1].Should().Be(new Size(2720, 80));
            result[2].Should().Be(new Size(2176, 64));
            result[3].Should().Be(new Size(2048, 60));
        }

        [Test]
        public void GetSteps_Should_Get_Correct_Steps_When_Many_Steps_Are_Needed()
        {
            // Arrange
            var calc = new StepCalculator(1);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(48);
            result[0].Should().Be(new Size(1024, 768));
            result[1].Should().Be(new Size(1014, 760));
            result[2].Should().Be(new Size(1004, 753));
            result[3].Should().Be(new Size(994, 745));
            result[4].Should().Be(new Size(984, 738));
            result[5].Should().Be(new Size(974, 730));
            result[6].Should().Be(new Size(964, 723));
            result[7].Should().Be(new Size(954, 716));
            result[8].Should().Be(new Size(945, 709));
            result[9].Should().Be(new Size(935, 702));
            result[10].Should().Be(new Size(926, 695));
            result[11].Should().Be(new Size(917, 688));
            result[12].Should().Be(new Size(908, 681));
            result[13].Should().Be(new Size(899, 674));
            result[14].Should().Be(new Size(890, 667));
            result[15].Should().Be(new Size(881, 661));
            result[16].Should().Be(new Size(872, 654));
            result[17].Should().Be(new Size(863, 647));
            result[18].Should().Be(new Size(855, 641));
            result[19].Should().Be(new Size(846, 634));
            result[20].Should().Be(new Size(838, 628));
            result[21].Should().Be(new Size(829, 622));
            result[22].Should().Be(new Size(821, 616));
            result[23].Should().Be(new Size(813, 609));
            result[24].Should().Be(new Size(805, 603));
            result[25].Should().Be(new Size(796, 597));
            result[26].Should().Be(new Size(789, 591));
            result[27].Should().Be(new Size(781, 585));
            result[28].Should().Be(new Size(773, 580));
            result[29].Should().Be(new Size(765, 574));
            result[30].Should().Be(new Size(757, 568));
            result[31].Should().Be(new Size(750, 562));
            result[32].Should().Be(new Size(742, 557));
            result[33].Should().Be(new Size(735, 551));
            result[34].Should().Be(new Size(728, 546));
            result[35].Should().Be(new Size(720, 540));
            result[36].Should().Be(new Size(713, 535));
            result[37].Should().Be(new Size(706, 529));
            result[38].Should().Be(new Size(699, 524));
            result[39].Should().Be(new Size(692, 519));
            result[40].Should().Be(new Size(685, 514));
            result[41].Should().Be(new Size(678, 509));
            result[42].Should().Be(new Size(671, 504));
            result[43].Should().Be(new Size(665, 499));
            result[44].Should().Be(new Size(658, 494));
            result[45].Should().Be(new Size(651, 489));
            result[46].Should().Be(new Size(645, 484));
            result[47].Should().Be(new Size(640, 480));
        } 
        
        [Test]
        public void GetSteps_Should_Stop_When_Max_Steps_Is_Reached()
        {
            // Arrange
            var calc = new StepCalculator(1);

            var start = new Size(2048, 1536);

            // Act
            var result = calc.GetSteps(start, 4);

            // Assert
            result.Count.Should().Be(StepCalculator.MaxSteps);
            result.Last().Should().Be(new Size(4, 3));
        }

        [Test]
        public void GetSteps_Should_Return_1_Step_When_Step_Percentage_Is_100()
        {
            // Arrange
            var calc = new StepCalculator(100);

            var start = new Size(3000, 2000);

            // Act
            var result = calc.GetSteps(start, 12);

            // Assert
            result.Count.Should().Be(2);
            result[0].Should().Be(new Size(3000, 2000));
            result[1].Should().Be(new Size(12, 8));
        }

        [Test]
        public void GetSteps_Should_Return_1_Step_When_Step_Percentage_Is_Greater_Than_100()
        {
            // Arrange
            var calc = new StepCalculator(110);

            var start = new Size(3000, 2000);

            // Act
            var result = calc.GetSteps(start, 12);

            // Assert
            result.Count.Should().Be(2);
            result[0].Should().Be(new Size(3000, 2000));
            result[1].Should().Be(new Size(12, 8));
        }

        [Test]
        public void GetSteps_Should_Use_Supplied_Step_Percentage()
        {
            // Arrange
            const int stepPercentage = 24;
            var calc = new StepCalculator(stepPercentage);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(3);
            result[0].Should().Be(new Size(1024, 768));
            result[1].Should().Be(new Size(778, 584));

            ((double)result[1].Width / (double)result[0].Width).Should().Be.RoughlyEqualTo((100 - stepPercentage) / 100.0, 0.0005);
        }

        [Test]
        public void GetSteps_Should_Use_Default_Step_Percentage_If_Supplied_Percentage_Is_Zero()
        {
            // Arrange
            var calc = new StepCalculator(0);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(4);
            result[0].Should().Be(new Size(1024, 768));
            result[1].Should().Be(new Size(819, 614));

            ((double)result[1].Width / (double)result[0].Width).Should().Be.RoughlyEqualTo((100 - StepCalculator.DefaultStepPercentage) / 100.0, 0.0005);
        }

        [Test]
        public void GetSteps_Should_Use_Default_Step_Percentage_If_Supplied_Percentage_Is_Less_Than_Zero()
        {
            // Arrange
            var calc = new StepCalculator(-1);

            var start = new Size(1024, 768);

            // Act
            var result = calc.GetSteps(start, 640);

            // Assert
            result.Count.Should().Be(4);
            result[0].Should().Be(new Size(1024, 768));
            result[1].Should().Be(new Size(819, 614));

            ((double)result[1].Width / (double)result[0].Width).Should().Be.RoughlyEqualTo((100 - StepCalculator.DefaultStepPercentage) / 100.0, 0.0005);
        }
    }
}
