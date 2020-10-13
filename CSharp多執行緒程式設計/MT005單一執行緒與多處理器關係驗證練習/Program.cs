using System;
using System.Diagnostics;

namespace MT005單一執行緒與多處理器關係驗證練習
{
    class Program
    {
        static Random random = new Random();
        static int maxLoop = int.MaxValue;
        static void Main(string[] args)
        {
            double foo, bar;
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)7;
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)224;
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)128;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < maxLoop/2; j++)
                {
                    foo = random.NextDouble() * 100 * random.NextDouble();
                    bar = foo * Math.PI;
                    foo = foo + bar;
                }
            }
            Console.WriteLine($"MT005 花費時間 {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
