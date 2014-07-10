using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProjectEuler.Common;

namespace Problem013
{
    /// <summary>
    /// Work out the first ten digits of the sum of the following one-hundred 50-digit numbers.
    /// 37107287533902102798797998220837590246510135740250
    /// 46376937677490009712648124896970078050417018260538
    /// .
    /// .
    /// [snip]
    /// .
    /// .
    /// 20849603980134001723930671666823555245252804609722
    /// 53503534226472524250874054075591789781264330331690
    /// </summary>
    class Program
    {
        static void Main(string[] args) {
            StreamReader sin = File.OpenText("numbers.txt");
            BigUInt answer = new BigUInt(0);

            string number = sin.ReadLine();
            while (!string.IsNullOrEmpty(number)) {
                answer = answer.Add(new BigUInt(number));
                number = sin.ReadLine();
            }
            
            sin.Close();

            Console.Out.WriteLine(answer.ToString());

            Console.Out.WriteLine("Klaar");
            Console.In.ReadLine();
        }

    }
}
