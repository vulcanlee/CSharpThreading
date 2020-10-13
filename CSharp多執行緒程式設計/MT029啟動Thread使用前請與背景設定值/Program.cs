using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT029啟動Thread使用前請與背景設定值
{
    // 啟動 Thread，並且指定皆為 背景執行緒
    // 在 Main 方法中，只是啟動該執行緒，之後 主執行緒 就會結束了，因此，不等到 背景執行緒 結束，App會自動終止所有的 背景執行緒
    class Program
    {
        public static void 執行緒方法()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("背景執行緒方法: {0}", i);
                Thread.Sleep(1000);
            }
        }

        public static void Main()
        {
            Thread backgroundThread = new Thread(()=>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("背景執行緒方法: <{0}>", i);
                    Thread.Sleep(1000);
                }
            });
            // 指定該執行緒為背景執行緒
            backgroundThread.IsBackground = true;

            // 當建立一個 Thread 物件，預設為前景執行緒
            Thread foregroundThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("前景執行緒方法: {0}", i);
                    Thread.Sleep(600);
                }
            });
            // 啟動該執行緒
            backgroundThread.Start();
            foregroundThread.Start();

            // 由於 backgroundThread 執行緒為背景執行緒
            // 在底下主執行緒程式碼執行完畢後，由於還有一個前景執行緒 foregroundThread
            // 因此，等到 foregroundThread 執行完畢後，不管背景執行緒是否已經執行完畢，
            // 會將所有背景執行緒移除掉
            Console.WriteLine("主執行緒要結束執行了");
        }
    }
}
