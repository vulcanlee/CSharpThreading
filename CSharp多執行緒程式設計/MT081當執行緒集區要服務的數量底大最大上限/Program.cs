using System;
using System.Threading;

namespace MT081當執行緒集區要服務的數量底大最大上限
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(8, 8);
            ThreadPool.SetMaxThreads(16, 16);

            for (int i = 0; i < 30; i++)
            {
                int idx = i;
                Console.WriteLine($"安排第{idx}個執行緒");
                var success = ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.WriteLine($"  第{idx}個執行緒開始執行");
                    Thread.Sleep(20 * 1000);
                    Console.WriteLine($"     第{idx}個執行緒執行完成");
                });
                if (success == false)
                {
                    Console.WriteLine($"第{idx}個執行緒無法排入執行緒集區佇列內");
                }
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
