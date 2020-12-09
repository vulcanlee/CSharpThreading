using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MT076體驗執行緒集區的參數調整意義
{
    class Program
    {
        static List<(DateTime current, int NumberOfThreads)> threadUsage =
            new List<(DateTime current, int NumberOfThreads)>();
        static void Main(string[] args)
        {
            int MaxJobs = 200;
            int SLEEP = 5 * 1000;
            bool stopMonitor = false;
            int MaxThreads=100; // 執行緒集區內，最多可以並行執行多少執行緒
            int MinThreads=20; // 執行緒集區內，事前要準備多少執行緒
            int workerThreads, completionPortThreads;
            CountdownEvent cde = new CountdownEvent(MaxJobs);

            #region 調整 ThreadPool 的條件
            #region 查詢現在執行緒集區的設定參數
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"ThreadPool Max Threads {workerThreads} / {completionPortThreads}");
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"ThreadPool Min Threads {workerThreads} / {completionPortThreads}");
            #endregion

            #region 設定執行緒集區內，最多可以並行執行多少執行緒
            Console.WriteLine($"開始調整執行緒集區的運作參數");
            ThreadPool.SetMinThreads(MaxThreads, completionPortThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"ThreadPool Max Threads {workerThreads} / {completionPortThreads}");
            #endregion
            #region 設定執行緒集區內，事前要準備多少執行緒
            ThreadPool.SetMinThreads(MinThreads, completionPortThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"ThreadPool Min Threads {workerThreads} / {completionPortThreads}");
            #endregion
            #endregion

            #region 建立與統計最多執行緒數量的執行緒
            Thread monitorWorker = new Thread(() =>
            {
                while (!stopMonitor)
                {
                    Thread.Sleep(200);
                    threadUsage.Add((DateTime.Now,
                        Process.GetCurrentProcess().Threads.Count));
                }
            });
            monitorWorker.Start();
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Parallel.For(0, MaxJobs, _ =>
            //{
            //    Thread.Sleep(SLEEP);
            //});
            for (int i = 0; i < MaxJobs; i++)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Thread.Sleep(SLEEP);
                    cde.Signal();
                });
            }
            cde.Wait();
            stopwatch.Stop();
            stopMonitor = true;
            Console.WriteLine();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine($"Max {threadUsage.Max(x => x.NumberOfThreads)} Threads");
        }
    }
}
