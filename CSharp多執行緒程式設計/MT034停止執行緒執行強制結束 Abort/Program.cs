using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT034停止執行緒執行強制結束_Abort
{
    public static class Program
    {
        public static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Console.WriteLine("執行緒執行中...");
                    Thread.Sleep(1000);
                }
                try
                {
                    // 請嘗試將上面無窮迴圈，搬移到這裡來執行，看看會有甚麼結果？
                }
                catch (ThreadAbortException ex)
                {
                    Console.WriteLine("有異常發生:" + ex.Message);
                    // 因為該執行緒已經強制終止，所以，在此做些清除的工作
                }
            }));
            thread.Start();
            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();
            // 發出終止請求
            // 於被叫用的所在執行緒中引發 ThreadAbortException，開始處理執行緒的結束作業
            thread.Abort();

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
