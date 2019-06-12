using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ParallelProgramming.ConsoleApp2
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
            {
                Console.WriteLine("Cancelation has been requested");
            });
            
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle requested, cancelation was requested");
            });

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("App ends here");
            Console.ReadKey();
        }
    }
}
