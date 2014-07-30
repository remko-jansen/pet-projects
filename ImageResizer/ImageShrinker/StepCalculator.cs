using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageShrinker
{
    public class StepCalculator
    {
        private int _stepPercentage;

        public const int DefaultStepPercentage = 25;
        public const int MaxSteps = 100;

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

        public List<Size> GetSteps(Size startSize, int goalLongestSide)
        {
            var result = new List<Size> {startSize};

            var isWidthLongestSide = startSize.Width >= startSize.Height;
            var dontNeedToResize = isWidthLongestSide ? startSize.Width <= goalLongestSide : startSize.Height <= goalLongestSide;

            if (dontNeedToResize)
                return result;

            var shrinkFactor = (100.0 - _stepPercentage) / 100.0;
            var runningShrinkfactor = shrinkFactor;
            bool done;
            do
            {
                var nextSize = Scale(startSize, runningShrinkfactor);
                runningShrinkfactor = runningShrinkfactor * shrinkFactor;

                done = isWidthLongestSide ? (nextSize.Width <= goalLongestSide) : (nextSize.Height <= goalLongestSide);
                done |= result.Count == MaxSteps - 1;

                if (done)
                    nextSize = Scale(startSize, goalLongestSide);

                result.Add(nextSize);
            } while (!done);

            return result;
        }

        private Size Scale(Size sizeIn, double factor)
        {
            var scaledWidth = Convert.ToInt32(Math.Round(sizeIn.Width * factor));
            var scaledHeight = Convert.ToInt32(Math.Round(sizeIn.Height * factor));

            var sizeOut = new Size(scaledWidth, scaledHeight);
            return sizeOut;
        }

        private Size Scale(Size sizeIn, int newLongestSide)
        {
            var isWidthLongestSide = sizeIn.Width >= sizeIn.Height;

            var factor = isWidthLongestSide ? ((double)newLongestSide) / sizeIn.Width : ((double)newLongestSide) / sizeIn.Height;

            var sizeOut = Scale(sizeIn, factor);
            return sizeOut;
        }
    }
}
