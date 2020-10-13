using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT036停止執行緒執行取消來源權杖
{
    // 要停止一個執行緒的執行，可以使用 取消許可證來源 CancellationTokenSource 物件
    public static class Program
    {
        public static void Main()
        {
            //定義 取消許可證來源
            CancellationTokenSource fooCTS = new CancellationTokenSource();
            Thread Thread = new Thread(new ThreadStart(() =>
            {
                // 判斷 取消許可證來源 的 [許可證 Token] 是否已經有發出取消請求
                while (fooCTS.Token.IsCancellationRequested == false)
                {
                    Console.WriteLine("執行緒執行中...");
                    Thread.Sleep(1000);
                }
            }));

            Thread.Start();
            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();
            // 發出取消請求
            fooCTS.Cancel();
            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
