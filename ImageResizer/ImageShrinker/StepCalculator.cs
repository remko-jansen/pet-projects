using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShrinker
{
    public class StepCalculator
    {
        private int _stepPercentage;

        public const int DefaultStepPercentage = 25;

        public StepCalculator()
        {
            _stepPercentage = DefaultStepPercentage;
        }

        public StepCalculator(int stepPercentage)
        {
            _stepPercentage = stepPercentage;

            if (_stepPercentage <= 0)
                _stepPercentage = DefaultStepPercentage;

            if (_stepPercentage > 100)
                _stepPercentage = 100;
        }

        public List<Size> GetSteps(Size startSize, int goal)
        {
            var result = new List<Size> {startSize};
            var stepShrinkage = (100.0 - _stepPercentage) / 100.0;
            var totalShrinkage = stepShrinkage;
            var shrinkWidth = startSize.Width >= startSize.Height;

            var done = false;
            do
            {
                var nextWidth = Convert.ToInt32(Math.Round(startSize.Width * totalShrinkage));
                var nextHeight = Convert.ToInt32(Math.Round(startSize.Height * totalShrinkage));

                var nextSize = new Size(nextWidth, nextHeight);
                result.Add(nextSize);

                totalShrinkage = totalShrinkage*stepShrinkage;

                done = shrinkWidth ? (nextWidth < goal) : (nextHeight < goal);
            } while (!done);

            return result;
        }
    }
}
