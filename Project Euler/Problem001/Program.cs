using System;
using System.Collections.Generic;
using System.Text;

namespace Problem001
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Find the sum of all the multiples of 3 or 5 below 1000.");
            
            int sum = 0;
            for (int i = 1; i<1000; i++) {
                if (IsDivisibleBy3(i) || IsDivisibleBy5(i)) {
                    if (sum == 0) {
                        Console.Write(i);
                    }
                    else {
                        Console.Write("+{0}",i);
                    }
                    sum += i;
                }
            }
            Console.WriteLine();
            Console.WriteLine("={0}", sum);
            
            Console.ReadKey();
        }

        static bool IsDivisibleBy5(int theNumber) {
            return (theNumber%5) == 0;
        }
        
        static bool IsDivisibleBy3(int theNumber) {
            return (theNumber%3) == 0;
        }
    }
}
