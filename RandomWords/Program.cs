using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RandomWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(randomString(-114271847) + " " + randomString(-852581083));
            Console.WriteLine(randomString(6548182) + " " +randomString(53316) + " " + randomString(509684) + " " + randomString(1092253770));
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            var words = new HashSet<string> { "remko", "jansen" };
            Console.WriteLine("Trying to find the words: " + string.Join(", ", words));
            findRandomWords(words);
            Console.WriteLine("Finished. Press any key...");
            Console.ReadKey();
        }

        static void findRandomWords(ICollection<string> wordSet)
        {
            var sw = new Stopwatch();  
            sw.Start();

            for (var seed = 0; seed < int.MaxValue; seed++)
            {
                if (seed % 100000 == 0)
                {
                    DisplayProgress(seed);
                }

                var word1 = randomString(seed);
                var word2 = randomString(-seed);

                if (wordSet.Contains(word1))
                {
                    Console.WriteLine();
                    Console.WriteLine(word1 + ": " + seed + " (" + sw.ElapsedMilliseconds + " ms)");
                    wordSet.Remove(word1);

                    if (wordSet.Count == 0)
                        break;
                }

                if (wordSet.Contains(word2))
                {
                    Console.WriteLine();
                    Console.WriteLine(word2 + ": " + -seed + " (" + sw.ElapsedMilliseconds + " ms)");
                    wordSet.Remove(word2);

                    if (wordSet.Count == 0)
                        break;
                }
            }
        }

        static void DisplayProgress(int seed)
        {
            var p = seed/(double)int.MaxValue;

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("{0:##0.000}%  ", p * 100);
            
        }

        static string randomString(int seed)
        {
            var r = new Random(seed);
            var s = new StringBuilder();

            int k;
            do
            {
                k = r.Next(27);
                if (k > 0)
                    s.Append((char)('a' + k - 1));
            } while (k != 0);

            return s.ToString();
        }

    }
}
