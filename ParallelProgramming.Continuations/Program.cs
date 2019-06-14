using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task1 = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAny(new[] { task1, task2 },
                t=>
                {
                    Console.WriteLine("Task has been completed:");
                    //foreach (var t in tasks)
                        Console.WriteLine(" - " + t.Result);
                    Console.WriteLine("All tasks done");
                });
            task3.Wait();
        }
    }
}