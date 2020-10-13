using System;
using System.Threading;

namespace MT052解除死結之使用定時器做到逾時解除鎖定
{
    class Program
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            object lockB = new object();

            // 建立第一個執行緒，先鎖定 A，在鎖定 B
            Thread thread1 = new Thread(() =>
            {
                while (true)
                {
                    bool enteredA = Monitor.TryEnter(lockA, TimeSpan.FromMilliseconds(3000));
                    if (!enteredA)
                    {
                        Console.WriteLine($"執行緒1發現死結A，放棄鎖定"); break;
                    }

                    Console.WriteLine("執行緒1 鎖定了 lockA");
                    Thread.Sleep(2000);
                    while (true)
                    {
                        bool enteredB = Monitor.TryEnter(lockB, TimeSpan.FromMilliseconds(3000));
                        if (!enteredB)
                        {
                            Console.WriteLine($"執行緒1發現死結B，放棄鎖定"); break;
                        }
                        Console.WriteLine("執行緒1 鎖定了 lockA 接著鎖定了 lockB");
                        Monitor.Exit(lockB);
                        break;
                    }
                    Monitor.Exit(lockA);
                    break;
                }
            });

            // 建立第二個執行緒，先鎖定 B，在鎖定 A
            Thread thread2 = new Thread(() =>
            {
                while (true)
                {
                    bool enteredB = Monitor.TryEnter(lockB, TimeSpan.FromMilliseconds(3000));
                    if (!enteredB)
                    {
                        Console.WriteLine($"執行緒2發現死結B，放棄鎖定"); break;
                    }
                    Console.WriteLine("執行緒2 鎖定了 lockB");
                    while (true)
                    {
                        bool enteredA = Monitor.TryEnter(lockA, TimeSpan.FromMilliseconds(3000));
                        if (!enteredA)
                        {
                            Console.WriteLine($"執行緒2發現死結A，放棄鎖定"); break;
                        }
                        Console.WriteLine("執行緒2 鎖定了 lockB 接著鎖定了 lockA");
                        Monitor.Exit(lockA);
                        break;
                    }
                    Monitor.Exit(lockB);
                    break;
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
