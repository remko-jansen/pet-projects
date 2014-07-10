using System;
using System.Collections.Generic;
using System.Text;

namespace Problem074
{
    /// <summary>
    /// Problem 74:
    /// Determine the number of factorial chains that contain exactly sixty non-repeating terms.
    /// </summary>
    class Program
    {
        static void Main(string[] args) {
            List<Chain> chains60 = new List<Chain>();
                
            for (long l = 1; l < 1000000; l++ ) {
                Chain c = new Chain(l);
                c.Calculate();

                if (c.Values.Count == 60) {
                    chains60.Add(c);
                }
            }
            Console.Out.WriteLine("No of 60 chains = {0}", chains60.Count);

            Console.Out.WriteLine("Klaar");
            Console.In.ReadLine();
        }
        
        
        public class Chain
        {
            private long _start;
            private List<long> _chain;


            public Chain(long start) {
                _start = start;
                _chain = new List<long>(60);
            }


            public long Start {
                get { return _start; }
            }

            public List<long> Values {
                get { return _chain; }
            }

            public void Calculate() {
                bool isRepeating = false;
                long nextValue = _start;

                _chain.Add(_start);
                while (!isRepeating) {
                    nextValue = GetFactorialSum(nextValue);
                    
                    if (_chain.Contains(nextValue)) {
                        isRepeating = true;
                    } else {
                        _chain.Add(nextValue);
                    }
                }
            }
            
            private long GetFactorialSum(long l) {
                if (l == 0)
                    return Factorial(l);

                long sum = 0;
                while (l > 0) {
                    int digit = (int)l % 10;
                    sum += Factorial(digit);

                    l = l / 10;
                }

                return sum;
            }

            private long Factorial(long l) {
                switch (l) {
                    case 0:
                        return 1;
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 6;
                    case 4:
                        return 24;
                    case 5:
                        return 120;
                    case 6:
                        return 720;
                    case 7:
                        return 5040;
                    case 8:
                        return 40320;
                    case 9:
                        return 362880;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            public override string ToString() {
                StringBuilder sb = new StringBuilder();
                foreach (long l in _chain) {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(l);
                }
                return sb.ToString();
                
            }
           
        }
    }
}
