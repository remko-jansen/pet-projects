using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Common;

namespace Problem025
{
    /// <summary>
    /// Project Euler [www.projecteuler.net]
    /// Problem 25
    /// What is the first term in the Fibonacci sequence to contain 1000 digits?
    /// </summary>
    class Program
    {
        static void Main(string[] args) {
            BigUInt fnminus2 = new BigUInt(1);
            BigUInt fnminus1 = new BigUInt(1);
            
            for (int i = 3; i<=1000; i++) {
                BigUInt fn = new BigUInt(fnminus2);
                fn.Add(fnminus1);
            }
        }
    }
}
