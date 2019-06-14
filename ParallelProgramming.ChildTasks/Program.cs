using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming.ChildTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                //detached task
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task staring");
                    Thread.Sleep(2000);
                    Console.WriteLine("Child task finishing");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hurray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Ooops, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);
                
                child.Start();
            }); 

            parent.Start();

            try
            {
                parent.Wait();
            }catch(AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}
