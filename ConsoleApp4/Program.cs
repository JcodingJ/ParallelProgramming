using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var item in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handeled elsewhere: {item.GetType()}");
                }
            }

        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("cant do this") { Source = "t" };
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("cant acces this!") { Source = "t2" };
            });


            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("invalid op!");
                        return true;
                    }
                    else return false;
                });
            }
        }
    }
}
