using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT013當WebClient完成後_會用何種執行緒為
{
    class Program
    {
        public static Semaphore semaphore;
        public static int MinThreadsOnThreadPool = 0;
        public static ConcurrentDictionary<int, int> ThreadsOnThreadPool = new ConcurrentDictionary<int, int>();
        public static int ThreadPoolCollectionTime1 = 30;
        public static int ThreadPoolCollectionTime2 = 30;

        public static int AvailableWorkerThreads = 0;
        public static int MaxRunningWorkThreads = 0;
        public static string APIEndPoint = "https://lobworkshop.azurewebsites.net/api/RemoteSource/AddSync/8/9/2000";
        static void Main(string[] args)
        {

            ThreadPoolInformation threadPoolInformation = new ThreadPoolInformation();
            GetThreadPoolInformation(threadPoolInformation);
            AvailableWorkerThreads = threadPoolInformation.AvailableWorkerThreads;

            FindDefaultThreadOnThreadPool();

            Console.WriteLine(); Console.WriteLine();

            // 複製一份執行前的 ThreadPool 資訊
            ThreadPoolInformation threadPoolCurrentInformation = threadPoolInformation.Clone();
            ShowCurrentThreadUsage(threadPoolInformation, threadPoolCurrentInformation);
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                Thread.Sleep(500);
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine("已經完成 WebClient 呼叫");
                int threadId = Thread.CurrentThread.ManagedThreadId;
                if (ThreadsOnThreadPool.ContainsKey(threadId))
                {
                    Console.WriteLine(">>>>   該執行緒為執行緒集區內的 工作執行緒 Worker Thread");
                }
                else
                {
                    Console.WriteLine("}}}}}   該執行緒為執行緒集區內的 工作執行緒 I/O Thread");
                }

                ShowCurrentThreadUsage(threadPoolInformation, threadPoolCurrentInformation);
            };
            webClient.DownloadStringAsync(new Uri(APIEndPoint));
            Thread.Sleep(500);
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("等候 WebClient 呼叫完成時候的執行緒集區狀態");
            ShowCurrentThreadUsage(threadPoolInformation, threadPoolCurrentInformation);


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

        }
        private static void FindDefaultThreadOnThreadPool()
        {
            int workerThreads;
            int completionPortThreads;
            // 傳回之執行緒集區的現在還可以容許使用多少的執行緒數量大小
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            MinThreadsOnThreadPool = workerThreads;
            CountdownEvent countdown = new CountdownEvent(MinThreadsOnThreadPool);
            semaphore = new Semaphore(0, MinThreadsOnThreadPool);
            for (int i = 0; i < MinThreadsOnThreadPool; i++)
            {
                int idx = i;
                ThreadPool.QueueUserWorkItem(x =>
                {
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    Thread.CurrentThread.Name = $"預設執行緒 {idx}";
                    Console.WriteLine($"Thread{threadId} 已經從執行緒集區取得該執行緒");
                    ThreadsOnThreadPool.TryAdd(threadId, threadId);
                    countdown.Signal();
                    semaphore.WaitOne();
                    Console.WriteLine($"Thread{threadId} 已經歸還給執行緒集區");
                });
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("等候取得所有執行緒都從執行緒集區取得...");
            countdown.Wait();

            Console.WriteLine("準備把取得的執行緒歸還給執行緒集區...");
            semaphore.Release(workerThreads);
            Thread.Sleep(2000);
        }
        private static void ShowCurrentThreadUsage(ThreadPoolInformation threadPoolInformation, ThreadPoolInformation threadPoolCurrentInformation)
        {
            int workerThreads;
            int completionPortThreads;
            // 傳回之執行緒集區的現在還可以容許使用多少的執行緒數量大小
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            threadPoolCurrentInformation.AvailableWorkerThreads = workerThreads;
            threadPoolCurrentInformation.AvailableCompletionPortThreads = completionPortThreads;
            threadPoolCurrentInformation.BusyWorkerThreads = threadPoolInformation.AvailableWorkerThreads - workerThreads;
            threadPoolCurrentInformation.BusyCompletionPortThreads = threadPoolInformation.AvailableCompletionPortThreads - completionPortThreads;
            ShowAvailableThreadPoolInformation(threadPoolCurrentInformation);
        }
        // 取得執行緒集區內的相關設定參數
        static void GetThreadPoolInformation(ThreadPoolInformation threadPoolInformation)
        {
            int workerThreads;
            int completionPortThreads;

            // 傳回之執行緒集區的現在還可以容許使用多少的執行緒數量大小
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            threadPoolInformation.AvailableWorkerThreads = workerThreads;
            threadPoolInformation.AvailableCompletionPortThreads = completionPortThreads;

            // 擷取可並行使用之執行緒集區的要求數目。 超過該數目的所有要求會繼續佇列，直到可以使用執行緒集區執行緒為止
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            threadPoolInformation.MaxWorkerThreads = workerThreads;
            threadPoolInformation.MaxCompletionPortThreads = completionPortThreads;

            // 在切換至管理執行緒建立和解構的演算法之前，擷取執行緒集區隨著提出新要求，視需要建立的執行緒最小數目。
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            threadPoolInformation.MinWorkerThreads = workerThreads;
            threadPoolInformation.MinCompletionPortThreads = completionPortThreads;

            // 如果目前電腦包含多個處理器群組，則這個屬性會傳回可供 Common Language Runtime (CLR) 使用的邏輯處理器數目
            threadPoolInformation.ProcessorCount = System.Environment.ProcessorCount;
        }
        // 顯示執行緒集區內上有多少空間，可以用來增加新執行緒的數量
        static void ShowAvailableThreadPoolInformation(ThreadPoolInformation threadPoolInformation)
        {
            Console.WriteLine($"   WorkItem Thread :" +
                $" (Busy:{threadPoolInformation.BusyWorkerThreads}, Free:{threadPoolInformation.AvailableWorkerThreads}, Min:{threadPoolInformation.MinWorkerThreads}, Max:{threadPoolInformation.MaxWorkerThreads})");
            Console.WriteLine($"   IOCP Thread :" +
                $" (Busy:{threadPoolInformation.BusyCompletionPortThreads}, Free:{threadPoolInformation.AvailableCompletionPortThreads}, Min:{threadPoolInformation.MinCompletionPortThreads}, Max:{threadPoolInformation.MaxCompletionPortThreads})");
        }
    }
    // 儲存執行緒集區相關運作參數的類別
    public class ThreadPoolInformation : ICloneable
    {
        public int ProcessorCount { get; set; }
        public int AvailableWorkerThreads { get; set; }
        public int AvailableCompletionPortThreads { get; set; }
        public int BusyWorkerThreads { get; set; }
        public int BusyCompletionPortThreads { get; set; }
        public int MaxWorkerThreads { get; set; }
        public int MaxCompletionPortThreads { get; set; }
        public int MinWorkerThreads { get; set; }
        public int MinCompletionPortThreads { get; set; }

        public void ComputeBusyThreads(ThreadPoolInformation threadPoolInformation)
        {
            this.BusyWorkerThreads = threadPoolInformation.AvailableWorkerThreads - this.AvailableWorkerThreads;
            this.BusyCompletionPortThreads = threadPoolInformation.BusyCompletionPortThreads - this.BusyCompletionPortThreads;
        }
        public ThreadPoolInformation Clone()
        {
            ICloneable cloneable = this;
            return cloneable.Clone() as ThreadPoolInformation;
        }
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
