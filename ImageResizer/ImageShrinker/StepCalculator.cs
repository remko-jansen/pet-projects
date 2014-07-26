using System;
using System.Collections.Generic;
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
        }

        public List<int> GetSteps(int start, int goal)
        {
        }
    }
}
