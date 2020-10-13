using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT006使用執行緒集區完成MT004需求
{
    class Program
    {
        // 依據你電腦執行速度，自行設定要跑的迴圈大小
        static int maxLoop = int.MaxValue / 2;
        static long counter;
        static object sync = new object();
        static EventWaitHandle eventWaitHandle1 = new EventWaitHandle(false, EventResetMode.ManualReset);
        static EventWaitHandle eventWaitHandle2 = new EventWaitHandle(false, EventResetMode.ManualReset);
        static void Main(string[] args)
        {
            counter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ThreadPool.QueueUserWorkItem(x =>
            {
                Increment();
                eventWaitHandle1.Set();
            });
            ThreadPool.QueueUserWorkItem(x =>
            {
                Decrement();
                eventWaitHandle2.Set();
            });

            WaitHandle.WaitAll(new[] { eventWaitHandle1, eventWaitHandle2 });
            stopwatch.Stop();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counter}");
            Console.WriteLine($"MT006 花費時間 {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void Decrement()
        {
            int localCounter = 0;
            for (int i = 0; i < maxLoop; i++)
            {
                localCounter--;
            }
            lock (sync)
            {
                counter += localCounter;
            }
        }

        private static void Increment()
        {
            int localCounter = 0;
            for (int i = 0; i < maxLoop; i++)
            {
                localCounter++;
            }
            lock (sync)
            {
                counter += localCounter;
            }
        }
    }
}
