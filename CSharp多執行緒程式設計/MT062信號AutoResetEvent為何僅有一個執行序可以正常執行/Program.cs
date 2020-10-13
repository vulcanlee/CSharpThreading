using System;
using System.Threading;

namespace MT062信號AutoResetEvent為何僅有一個執行序可以正常執行
{
    class Program
    {
        private static AutoResetEvent MySyncEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            for (int i = 1; i <= 4; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            Console.WriteLine("按下任一按鍵，送出收到訊號狀態");
            Console.ReadKey();
            MySyncEvent.Set();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            Console.WriteLine($"@ 工作執行緒{state} 準備等候訊號通知");
            MySyncEvent.WaitOne();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行");
                Thread.Sleep(1000);
            }
        }
    }
}
