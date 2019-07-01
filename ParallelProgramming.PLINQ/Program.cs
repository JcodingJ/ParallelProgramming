using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelProgramming.PLINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int count = 50;

            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x=>
            {
                int newValue = x * x * x;
                Console.WriteLine($"{newValue} ({Task.CurrentId})");
                results[x - 1] = newValue;
            });
            Console.WriteLine();

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item} \t");
            //}
            //Console.WriteLine();
            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);

            foreach (var item in cubes)
            {
                Console.WriteLine($"{item} \t");
            }
        }
    }
}
