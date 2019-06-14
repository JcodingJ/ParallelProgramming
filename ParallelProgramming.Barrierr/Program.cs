using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.Barrierr
{
    static class Program
    {
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting kettle on (takes a bit longer)");
            Thread.Sleep(1000);
            barrier.SignalAndWait(); // 2
            Console.WriteLine("Puring water into the cup."); // 0
            barrier.SignalAndWait(); // 1
            Console.WriteLine("Putting the kettle away");
        }
        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            barrier.SignalAndWait(); // 1
            Console.WriteLine("Adding tea to the cup");
            barrier.SignalAndWait(); // 2
            Console.WriteLine("Adding sugar");

        }
        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
             {
                 Console.WriteLine("Enjoy Your cup of tea.");
             });
            tea.Wait();

        }
    }
}
