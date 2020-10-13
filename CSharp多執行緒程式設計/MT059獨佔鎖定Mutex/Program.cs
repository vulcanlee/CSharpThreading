using System;
using System.Threading;

namespace MT059獨佔鎖定Mutex
{
    class Program
    {
        private static Mutex mutex = new Mutex();
        private const int numThreads = 3;

        static void Main(string[] args)
        {
            // 產生多個執行緒，執行同一個方法
            for (int i = 0; i < numThreads; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadProc);
            }


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void ThreadProc(object state)
        {
            Console.WriteLine($"Thread{Thread.CurrentThread.ManagedThreadId} 等候取得 Mutex 擁有權 WaitOne()");
            mutex.WaitOne();

            Console.WriteLine($"Thread{Thread.CurrentThread.ManagedThreadId} @ 已經取得 Mutex 擁有權");

            Console.WriteLine($"模擬 Thread{Thread.CurrentThread.ManagedThreadId} 執行專屬獨享的1秒時間的工作");
            Thread.Sleep(1000);


            Console.WriteLine($"Thread{Thread.CurrentThread.ManagedThreadId} 準備要釋放 Mutex 擁有權");
            mutex.ReleaseMutex();
            Console.WriteLine($"Thread{Thread.CurrentThread.ManagedThreadId} # 已經要釋放 Mutex 擁有權 ReleaseMutex()");
        }
    }
}
