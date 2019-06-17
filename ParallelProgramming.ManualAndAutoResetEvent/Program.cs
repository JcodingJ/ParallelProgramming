using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.ManualAndAutoResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new ManualResetEventSlim(false);
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for water");
                evt.Wait();
                Console.WriteLine($"Here is Your tea");
                var ok = evt.Wait(1000);
                
                if(ok)
                    Console.WriteLine($"Enjoy Yoru Tea");
                else
                    Console.WriteLine($"No Tea For U");
            });
            makeTea.Wait();

        }
    }
}
