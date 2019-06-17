using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ParallelProgramming.ConcurentQueuee
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            int result;
            if (q.TryDequeue(out result))
                Console.WriteLine($"Removed element {result}");
            if (q.TryPeek(out result))
                Console.WriteLine($"Front element is {result}");

        }
    }
}
