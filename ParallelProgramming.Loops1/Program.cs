using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.Loops1
{
    //Invoke
    class Program
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i+=step)
            {
                yield return i;
            }
        }
        static void Main(string[] args)
        {
            var a = new Action(() =>
            {
                Console.WriteLine($"First {Task.CurrentId}");
            });
            var b = new Action(() =>
            {
                Console.WriteLine($"Second {Task.CurrentId}");
            });
            var c = new Action(() =>
            {
                Console.WriteLine($"Third {Task.CurrentId}");
            });
            Parallel.Invoke(a,b,c);


            var po = new ParallelOptions();
            po.CancellationToken = new System.Threading.CancellationToken();

            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"i^2 = {i * i}\t");
            });

            string[] words = { "oh", "what", "a", "night" };

            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has length {word.Length}, (task {Task.CurrentId})");
            });

            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine); 


        }
    }
}
