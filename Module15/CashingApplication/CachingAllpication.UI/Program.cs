using System;
using CashingApplication;

namespace CachingAllpication.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Fibonacci fib = new Fibonacci();

            while (true)
            {
                var fibonacci = fib.GetFibonacci(6);

                fibonacci.ForEach(n => Console.WriteLine(n));
                Console.ReadKey();
            }
        }
    }
}
