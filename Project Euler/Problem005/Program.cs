using System;
using System.Collections.Generic;
using System.Text;

namespace Problem005
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("What is the smallest number that is evenly divisible by all of the numbers from 1 to 20?");

            long numberToTest = 20;
            bool answerFound = false;
            long answer = 0;
            
            while (!answerFound) {
                if (numberToTest % 100000 == 0)
                    Console.WriteLine(numberToTest);

                int numberOfDividers = 0;
                for (long divider = 3; divider <= 20; divider++) {
                    if (!IsDivisibleBy(numberToTest, divider)) {
                        break; // break for loop
                    }
                    numberOfDividers++;
                }

                if (numberOfDividers == 18) {
                    answerFound = true;
                    answer = numberToTest;
                }
                
                numberToTest += 20;
            }
            
            Console.WriteLine();
            Console.WriteLine("The answer is: {0}", answer);
            Console.ReadKey();
        }

        static bool IsDivisibleBy(long theNumber, long divider) {
            return (theNumber%divider) == 0;
        }
    }
}
