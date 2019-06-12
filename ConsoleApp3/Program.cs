using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");

                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I am done");
            },token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            Task.WaitAll(new[] { t, t2 },4000);

            Console.WriteLine($"Task t sattus is {t.Status}");
            Console.WriteLine($"Task t2 sattus is {t2.Status}");

            Console.WriteLine("Main program is done");
        }
    }
}
