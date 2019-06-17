using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ParallelProgramming.CountDownn
{
    class Program
    {
        private static int tasksCount = 5;
        static CountdownEvent cte = new CountdownEvent(tasksCount);
        private static Random random = new Random();
        
        static void Main(string[] args)
        {
            for (int i = 0; i < tasksCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task  {Task.CurrentId}" );
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting task: {Task.CurrentId}");
                });
            }
            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"waiting for other tasks to complete in {Task.CurrentId}");
                cte.Wait();
                Console.WriteLine("All tasks completed");
            });
            finalTask.Wait();
        }
    }
}