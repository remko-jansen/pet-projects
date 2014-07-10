using System;
using System.Collections.Generic;
using System.Text;

namespace Problem002
{
    /// <summary>
    /// Problem 2:
    /// Find the sum of all the even-valued terms in the Fibonacci sequence which do not exceed four million.
    /// </summary>
    class Program
    {
        static void Main(string[] args) {
            long limit = 4000000; // 4 million
            long sum = 2;
            long termNminus1 = 1;
            long termN = 2;
            long termNplus1 = 0;

            while (termN <= limit) {
                termNplus1 = termNminus1 + termN;

                termNminus1 = termN;
                termN = termNplus1;

                
                if (termN <= limit && (termN % 2 == 0)) {
                    sum += termN;
                }
            }

            Console.Out.WriteLine("sum = {0}", sum);
            Console.Out.WriteLine("klaar");
            Console.In.ReadLine();
        }
    }
}
