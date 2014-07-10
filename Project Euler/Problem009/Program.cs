using System;
using System.Collections.Generic;
using System.Text;

namespace Problem009
{
    /// <summary>
    /// Problem 9
    /// Find the only Pythagorean triplet, {a, b, c}, for which a + b + c = 1000.
    /// </summary>
    class Program
    {
        static void Main(string[] args) {
            long goal = 100000;
            for (long a = 1; a <= goal - 2; a++) {
                for (long b = a + 1; b <= goal - a - 1; b++) {
                    long c = goal - a - b;
                    if (a * a + b * b == c * c) {
                        Console.Out.WriteLine("a={0} b={1} c={2}", a, b, c);
                        Console.Out.WriteLine("a*b*c={0}", a*b*c);
                    }
                }
            }
            Console.Out.WriteLine("klaar");
            Console.In.ReadLine();
        }
    }
}
