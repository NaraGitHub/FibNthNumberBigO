using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibNthNumberBigO
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                Console.Write("Enter[Enter 0 to exit]:");
                Int32 n = Convert.ToInt32(Console.ReadLine());
                if (n == 0) break;


                Parallel.Invoke(
                    () =>
                    {
                        var perf1 = new Stopwatch();
                        ulong result = 0;
                        //#1 Recursive 
                        perf1.Start();
                        result = FibonacciRecursive(n);
                        perf1.Stop();
                        Console.WriteLine($"Fib of {n}th value is {result}. TimeTaken:{perf1.Elapsed.TotalMilliseconds}");
                    },
                    () =>
                    {
                        var perf1 = new Stopwatch();
                        ulong result = 0;

                        //#2 Store or memoize
                        var memoizeArray = Enumerable.Repeat<ulong>(0, n + 1).ToArray();
                        memoizeArray[1] = 1;
                        memoizeArray[2] = 1;
                        perf1.Start();
                        result = FibonacciMemoize(n, memoizeArray);
                        perf1.Stop();
                        Console.WriteLine($"Fib of {n}th value is {result}, TimeTaken:{perf1.Elapsed.TotalMilliseconds}");
                    },
                    () =>
                    {

                        var perf1 = new Stopwatch();
                        ulong result = 0;

                        //#3 Bottom up
                        perf1.Start();
                        result = FibonacciBottomUp(n);
                        Console.WriteLine($"Fib of {n}th value is {result}, TimeTaken:{perf1.Elapsed.TotalMilliseconds}");
                        perf1.Stop();
                    }

                   );

                Console.WriteLine();

            }
        }

        // 1 1 2 3 5 8 13 
        static ulong FibonacciRecursive(int n)
        {
            ulong result = 0;

            // first, second position value is 1 only
            if (n == 1 || n == 2) result = 1;

            else result = FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);

            return result;

        }

        static ulong FibonacciMemoize(int n, ulong[] memoizeArray)
        {
            ulong result = 0;

            if (memoizeArray[n] != 0) return memoizeArray[n];


            if (n == 1 || n == 2) result = 1;
            else
            {
                result = FibonacciMemoize(n - 1, memoizeArray) + FibonacciMemoize(n - 2, memoizeArray);
                memoizeArray[n] = result;
            }
            return result;

        }

        static ulong FibonacciBottomUp(int n)
        {

            var bottomUp = new ulong[n + 1];
            bottomUp[0] = 0; //skip & declare to dummy value to patch zero index array
            bottomUp[1] = 1;
            bottomUp[2] = 1;

            for (var i = 3; i <= n; i++)
            {
                bottomUp[i] = bottomUp[i - 1] + bottomUp[i - 2];
            }
            return bottomUp[n];
        }

    }
}
