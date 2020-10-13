using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT050體驗多工作打死結
{
    // 這個範例展示了多非同步工作互相打死結的範例
    // 工作1 & 工作2 互相鎖定，導致無法解開
    class Program
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            object lockB = new object();

            //底下的程式碼為使用 Task 物件來時做出死結現象
            // 建立第一個工作
            var task1 = Task.Run(() =>
            {
                lock (lockA)
                {
                    Console.WriteLine("工作1 鎖定了 lockA");
                    Thread.Sleep(1000);
                    lock (lockB)
                    {
                        Console.WriteLine("工作1 鎖定了 lockA 接著鎖定了 lockB");
                    }
                }
            });
            // 建立第二個工作
            var task2 = Task.Run(() =>
            {
                lock (lockB)
                {
                    Console.WriteLine("工作2 鎖定了 lockB");
                    lock (lockA)
                    {
                        Console.WriteLine("工作2 鎖定了 lockB 接著鎖定了 lockA");
                    }
                }
            });

            Console.WriteLine("請按任一鍵，以結束執行");
            Console.ReadKey();
            //Task.WhenAll(task1, task2).Wait();
        }
    }
}
