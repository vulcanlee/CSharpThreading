using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT002使用多執行緒非同步執行遞增與遞減
{
    class Program
    {
        // 依據你電腦執行速度，自行設定要跑的迴圈大小
        static int maxLoop = int.MaxValue;
        static long counter;
        static void Main(string[] args)
        {
            //Process.GetCurrentProcess().ProcessorAffinity =
            // (IntPtr)0b1000_0000;
            counter = 0;
            Thread thread1 = new Thread(x =>
            {
                Increment();
            });
            Thread thread2 = new Thread(x =>
            {
                Decrement();
            });

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            thread1.Start(); thread2.Start();

            thread1.Join();
            thread2.Join();
            stopwatch.Stop();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counter}");
            Console.WriteLine($"MT002 花費時間 {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void Decrement()
        {
            for (int i = 0; i < maxLoop; i++)
            {
                counter--;
            }
        }

        private static void Increment()
        {
            for (int i = 0; i < maxLoop; i++)
            {
                counter++;
            }
        }
    }
}
