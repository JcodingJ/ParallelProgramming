using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.PLinq4
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sum = Enumerable.Range(1, 1000).Aggregate(0, (i, acc) => i + acc);

            var sum = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                    0,
                    (partialSum, i) => partialSum += i,
                    (total,subTotal)=>total+=subTotal,
                    i=>i
                );

            Console.WriteLine($"sum = {sum}");
        }
    }
}
