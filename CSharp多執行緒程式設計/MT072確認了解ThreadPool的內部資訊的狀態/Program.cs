using System;
using System.Threading;

namespace MT072確認了解ThreadPool的內部資訊的狀態
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(100, 100);
            ShowThreadPoolInformation();
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(x =>
                {
                    Thread.Sleep(2000);
                });
            }
            Thread.Sleep(1000);
            ShowThreadPoolInformation();
        }
        static void ShowThreadPoolInformation()
        {
            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   可用背景工作執行緒的數目 : {workerThreads} / 可用非同步 I/O 執行緒的數目 : { completionPortThreads}");
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   執行緒集區中的背景工作執行緒最大數目 : {workerThreads} / 執行緒集區中的非同步 I/O 執行緒最大數目 : { completionPortThreads}");
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   需要建立的背景工作執行緒最小數目 : {workerThreads} / 需要建立的非同步 I/O 執行緒最小數目 : { completionPortThreads}");
            Console.WriteLine($"");
        }
    }
}
