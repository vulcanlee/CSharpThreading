using System;
using System.Threading;

namespace MT051解除死結之使用相同順序來進行巢狀鎖定
{
    class Program
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            object lockB = new object();

            // 建立第一個執行緒，其會執行 DoWork1 方法
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

            // 建立第二個執行緒，其會執行 DoWork1 方法
            Thread thread2 = new Thread(() =>
            {
                lock (lockA)
                {
                    Console.WriteLine("執行緒2 鎖定了 lockA");
                    lock (lockB)
                    {
                        Console.WriteLine("執行緒2 鎖定了 lockA 接著鎖定了 lockB");
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
