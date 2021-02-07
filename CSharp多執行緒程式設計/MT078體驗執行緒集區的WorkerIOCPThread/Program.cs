using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MT078體驗執行緒集區的WorkerIOCPThread
{
    class Program
    {
        static int loopCount = 30;
        static int workThreadSleep = 0;
        static int ioThreadSleep = 5000;
        static int monitorThreadUsageSleep = 1000;

        static void Main(string[] args)
        {
            //SetThreadPoolConfiguration();
            PrintThreadPoolConfiguration();
            PrintSummaryThreadCounts();

            var tasks = new List<Task<string>>();

            for (int i = 0; i < loopCount; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    // 執行非同步作業前的強制休息
                    Thread.Sleep(workThreadSleep);
                    return IssueAsynchronousRequestAsync();
                }));
            }

            #region 若全部非同步工作尚未完成，每秒鐘列印執行緒集區使用情況
            var allComplete = Task.WhenAll(tasks);
            while (allComplete.Status != TaskStatus.RanToCompletion)
            {
                Thread.Sleep(monitorThreadUsageSleep);
                PrintSummaryThreadCounts();
            }
            #endregion
        }
        static async Task<string> IssueAsynchronousRequestAsync()
        {
            // https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,6b7ffca539f4a368
            var str = await new HttpClient().GetStringAsync("https://contososyncfusion.azurewebsites.net/");

            //// https://source.dot.net/#System.IO.FileSystem/System/IO/File.cs,321993cd4729625c
            //var str = await File.ReadAllTextAsync("ConsoleApp1.runtimeconfig.json");

            //string filename = @"ConsoleApp1.runtimeconfig.json";
            //byte[] result;
            //using (FileStream SourceStream = File.Open(filename, FileMode.Open))
            //{
            //    result = new byte[SourceStream.Length];
            //    await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            //}
            //var str = System.Text.Encoding.ASCII.GetString(result);

            //// https://source.dot.net/#System.Private.CoreLib/FileStream.cs,2c203718e302b739
            //byte[] result;
            //using (FileStream SourceStream = new FileStream("ConsoleApp1.runtimeconfig.json", FileMode.Open,
            //FileAccess.Read, FileShare.None, 4096, true))
            //{
            //    result = new byte[SourceStream.Length];
            //    await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            //}
            //var str = System.Text.Encoding.ASCII.GetString(result);

            //string str = "Async Result";
            //await Task.Delay(500);

            //var str = new HttpClient().GetStringAsync("https://contososyncfusion.azurewebsites.net/").Result;

            // 非同步作業完成後的強制休息
            Thread.Sleep(ioThreadSleep);
            return str;
        }
        static void PrintSummaryThreadCounts()
        {
            int ioThreads, minIoThreads, maxIoThreads, workerThreads, minWorkerThreads, maxWorkerThreads,
                threadCount, processThreadCount;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxIoThreads);
            ThreadPool.GetMinThreads(out minWorkerThreads, out minIoThreads);
            ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            threadCount = ThreadPool.ThreadCount;
            processThreadCount = Process.GetCurrentProcess().Threads.Count;

            #region 輸出執行緒使用情況
            Console.WriteLine($"Worker Threads : {maxWorkerThreads - workerThreads}");
            Console.WriteLine($"IO Threads : {maxIoThreads - ioThreads}");
            Console.WriteLine($"Total Process Used Threads : {processThreadCount}");
            Console.WriteLine($"Total ThreadPool Used Threads : {threadCount}");
            Console.WriteLine();
            #endregion

        }
        static void PrintThreadPoolConfiguration()
        {
            int ioThreads, minIoThreads, maxIoThreads, workerThreads, minWorkerThreads, maxWorkerThreads,
                threadCount, processThreadCount;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxIoThreads);
            ThreadPool.GetMinThreads(out minWorkerThreads, out minIoThreads);
            ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            threadCount = ThreadPool.ThreadCount;
            processThreadCount = Process.GetCurrentProcess().Threads.Count;

            #region 輸出執行緒集區現在設定參數
            Console.WriteLine($"***** Current ThreadPool Configuration *****");
            Console.WriteLine($"Available Worker / IO Threads : {workerThreads} / {ioThreads}");
            Console.WriteLine($"Min Worker / IO Threads : {minWorkerThreads} / {minIoThreads}");
            Console.WriteLine($"Max Worker / IO Threads : {maxWorkerThreads} / {maxIoThreads}");
            Console.WriteLine($"Total ThreadPool Used Threads : {threadCount}");
            Console.WriteLine(string.Join("", Enumerable.Repeat("-", 30)));
            Console.WriteLine();
            #endregion

        }
        static void SetThreadPoolConfiguration()
        {
            int ioThreads, minIoThreads, maxIoThreads, workerThreads, minWorkerThreads, maxWorkerThreads,
                threadCount, processThreadCount;

            #region 設定執行緒集區參數
            maxWorkerThreads = 1000;
            maxIoThreads = 8;
            ThreadPool.SetMaxThreads(maxWorkerThreads, maxIoThreads);
            #endregion

        }
    }
}
