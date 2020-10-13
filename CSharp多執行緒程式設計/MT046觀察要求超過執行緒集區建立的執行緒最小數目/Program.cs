using System;
using System.Threading;

namespace MT046觀察要求超過執行緒集區建立的執行緒最小數目
{
    class Program
    {
        static void Main(string[] args)
        {
            int ITERATIONS = 15;
            int threadSleep = (ITERATIONS+2) * 1000;
            Console.WriteLine(GetThreadPoolStatus());

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(x =>
                {
                    Console.WriteLine($"Idx={j} 執行緒開始執行 {DateTime.Now}");
                    Thread.Sleep(threadSleep);
                });
                Console.WriteLine($">> 成功在執行緒集區內排隊 : {j}");
                Thread.Sleep(10);
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static string GetThreadPoolStatus()
        {
            int workerThreadsAvailable;
            int completionPortThreadsAvailable;
            int workerThreadsMax;
            int completionPortThreadsMax;
            int workerThreadsMin;
            int completionPortThreadsMin;
            ThreadPool.GetAvailableThreads(out workerThreadsAvailable, out completionPortThreadsAvailable);
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);

            string threadStatus = $"AW:{workerThreadsAvailable} AC:{completionPortThreadsAvailable}" +
                $" MaxW:{workerThreadsMax} MaxC:{completionPortThreadsMax}" +
                $" MinW:{workerThreadsMin} MinC:{completionPortThreadsMin}";
            return threadStatus;

        }
    }
}
