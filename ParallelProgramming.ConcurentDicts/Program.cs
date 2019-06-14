using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.ConcurentDicts
{

    class Program
    {
        private static ConcurrentDictionary<string, string> capitals =
       new ConcurrentDictionary<string, string>();
        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main Thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not added")} the elemnet");
        }
        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            //capitals["Russia"] = "Leningrad";
            //capitals.AddOrUpdate("Russia", "Moscow",
            //    (k, old) => old + " -- > Moskow");

            //Console.WriteLine($"Capital of Russia is {capitals["Russia"]}");



            //capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"Capital of Sweden is {capOfSweden}");


            const string ToRemove = "Russia";

            string removed;
            bool didRemoved = capitals.TryRemove(ToRemove, out removed);

            if(didRemoved)
                Console.WriteLine($"Removed: {removed}");
            else
                Console.WriteLine($"Failed to remove: {ToRemove}");

            foreach (var item in capitals)
            {
                Console.WriteLine($" - {item.Value} is the capital of {item.Key} ");
            }
        }
    }
}
