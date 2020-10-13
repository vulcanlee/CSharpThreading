using System;
using System.Threading;

namespace MT065信號Barrier
{
    class Program
    {
        private static Barrier MySyncEvent = new Barrier(4, x =>
        {
            Console.WriteLine("所有執行緒都執行完成，需要重新開始");

        });
        static void Main(string[] args)
        {
            for (int i = 1; i <= 4; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"@ 工作執行緒{state} 準備等候訊號通知");
                Thread.Sleep(new Random().Next(800,5000));
                Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行");
                MySyncEvent.SignalAndWait();
            }
        }
    }
}
