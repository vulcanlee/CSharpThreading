using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT031封鎖等候執行緒完成AutoResetEvent
{
    class Program
    {
        static void Main()
        {
            // 建立 向等候的執行緒通知發生事件
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            #region 使用 ThreadPool 來請求一個新的執行緒
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPool委派方法), autoEvent);
            #endregion

            // 等候背景執行緒結束
            autoEvent.WaitOne();
            Console.WriteLine("我知道了，執行緒2，已經執行完成");

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void ThreadPool委派方法(object stateInfo)
        {
            Console.WriteLine("執行緒2，執行由ThreadPool類別內取得的執行緒");
            Thread.Sleep(2000);
            Console.WriteLine("執行緒2，完成");

            // 發出訊號，通知該執行緒已經成功完成執行了
            ((AutoResetEvent)stateInfo).Set();
        }
    }
}
