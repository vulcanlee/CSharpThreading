using System;
using System.Threading;

namespace MT049體驗多執行緒打死結
{
    // 這個範例展示了執行緒互相打死結的範例
    // 執行緒1 & 執行緒2 互相鎖定，導致無法解開
    class Program
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            object lockB = new object();

            // 建立第一個執行緒，先鎖定 A，在鎖定 B
            Thread thread1 = new Thread(() =>
            {
                lock (lockA)
                {
                    Console.WriteLine("執行緒1 鎖定了 lockA");
                    Thread.Sleep(2000);
                    lock (lockB)
                    {
                        Console.WriteLine("執行緒1 鎖定了 lockA 接著鎖定了 lockB");
                    }
                }
            });

            // 建立第二個執行緒，先鎖定 B，在鎖定 A
            Thread thread2 = new Thread(() =>
            {
                lock (lockB)
                {
                    Console.WriteLine("執行緒2 鎖定了 lockB");
                    lock (lockA)
                    {
                        Console.WriteLine("執行緒2 鎖定了 lockB 接著鎖定了 lockA");
                    }
                }
            });

            // 開始執行執行緒的委派方法
            thread1.Start();
            thread2.Start();

            Thread.Sleep(2500);
            Console.WriteLine("請按任一鍵，以結束執行");
            Console.ReadKey();
        }
    }
}
