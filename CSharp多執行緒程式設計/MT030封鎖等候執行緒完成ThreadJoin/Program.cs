using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT030封鎖等候執行緒完成ThreadJoin
{
    class Program
    {
        static void Main()
        {
            #region 使用 Thread 類別，產生一個新的執行緒
            Thread regularThread = new Thread(new ThreadStart(Thread委派方法));
            // 開啟執行該執行緒
            regularThread.Start();
            #endregion

            // 等候前景執行緒結束(這個執行緒是由 Thread 類別產生的)
            regularThread.Join();
            Console.WriteLine("我知道了，執行緒1，已經執行完成");

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void Thread委派方法()
        {
            Console.WriteLine("執行緒1，執行Thread類別產生的執行緒");
            Thread.Sleep(2000);
            Console.WriteLine("執行緒1，完成");
        }
    }
}
