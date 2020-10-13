using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT035停止執行緒執行輪詢Polling
{
    // 要停止一個執行緒的執行，可以使用一個變數旗標來控制，該執行緒是否需要繼續執行
    public static class Program
    {
        public static void Main()
        {
            bool stopped = false;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (!stopped)
                {
                    Console.WriteLine("執行緒執行中...");
                    Thread.Sleep(1000);
                }
            }));
            thread.Start();
            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();

            // 發出取消請求
            stopped = true;

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
