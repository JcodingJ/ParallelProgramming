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
        
        static void Main(string[] args)
        {
        }
    }
}
