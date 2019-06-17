using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.BreakingLoops
{
    class Program
    {
        private static ParallelLoopResult result;
        public static void Demo()
        {
            var cts = new CancellationTokenSource();
            var po = new ParallelOptions
            {
                CancellationToken = cts.Token
            };


            result = Parallel.For(0, 20,po, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x} [{Task.CurrentId}]\t");
                if (x == 10)
                {
                    cts.Cancel();
                    //throw new Exception();
                    //state.Break();
                }
            });


            Console.WriteLine();
            Console.WriteLine($"For loop completed {result.IsCompleted}");
            if(result.LowestBreakIteration.HasValue)
                Console.WriteLine($"lowest break iteration is {result.LowestBreakIteration}");
                
        }
        static void Main(string[] args)
        {
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce.CancellationToken); 
            }
            
        }
    }
}
