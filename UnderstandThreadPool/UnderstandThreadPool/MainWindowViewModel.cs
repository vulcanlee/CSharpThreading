using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace UnderstandThreadPool
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Init();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public int AvailableWorkerThreads { get; set; }
        public int AvailableIopcThreads { get; set; }
        public int MinWorkerThreads { get; set; }
        public int MinIopcThreads { get; set; }
        public int MaxWorkerThreads { get; set; }
        public int MaxIopcThreads { get; set; }
        public int TotalUseWorkerThreads { get; set; }
        public int TotalUseIocpThreads { get; set; }
        public int TotalProcessUseThreads { get; set; }
        public int TotalThreadPoolUseThreads { get; set; }
        public DelegateCommand 設定Command { get; set; }
        public DelegateCommand 開始執行Command { get; set; }
        public int LoopCount { get; set; } = 30;
        public int WorkThreadSleep { get; set; } = 0;
        public int IocpThreadSleep { get; set; } = 5000;
        public int MonitorThreadUsageSleep { get; set; } = 1000;
        public Visibility 開始執行CommandVisibility { get; set; } = Visibility.Visible;
        public string 量測執行緒使用情況Text { get; set; } = "開始 量測執行緒使用情況";
        public DelegateCommand 量測執行緒使用情況Command { get; set; }
        CancellationTokenSource cts = new CancellationTokenSource();
        public string Message { get; set; }
        private void Init()
        {
            GetThreadPoolConfiguration();

            量測執行緒使用情況Command = new DelegateCommand(() =>
            {
                if (量測執行緒使用情況Text == "開始 量測執行緒使用情況")
                {
                    量測執行緒使用情況Text = "停止 量測執行緒使用情況";
                    cts = new CancellationTokenSource();
                    Task.Run(async () =>
                    {
                        while (true)
                        {
                            if (cts.Token.IsCancellationRequested)
                            {
                                量測執行緒使用情況Text = "開始 量測執行緒使用情況";
                                break;
                            }
                            PrintSummaryThreadCounts();
                            await Task.Delay(MonitorThreadUsageSleep);
                        }
                    });
                }
                else
                {
                    cts.Cancel();
                }
            });
            設定Command = new DelegateCommand(() =>
            {
                ThreadPool.SetMaxThreads(MaxWorkerThreads, MaxIopcThreads);
                ThreadPool.SetMinThreads(MinWorkerThreads, MinIopcThreads);
                GetThreadPoolConfiguration();
            });
            開始執行Command = new DelegateCommand(async () =>
            {
                開始執行CommandVisibility = Visibility.Hidden;
                Message = "";
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var tasks = new List<Task<string>>();

                for (int i = 0; i < LoopCount; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        // 執行非同步作業前的強制休息
                        Thread.Sleep(WorkThreadSleep);
                        return IssueAsynchronousRequestAsync();
                    }));
                }

                #region 若全部非同步工作尚未完成，每秒鐘列印執行緒集區使用情況
                var allComplete = Task.WhenAll(tasks);
                while (allComplete.Status != TaskStatus.RanToCompletion)
                {
                    await Task.Delay(MonitorThreadUsageSleep);
                    //PrintSummaryThreadCounts();
                }
                #endregion
                stopwatch.Stop();
                Message = $"花費時間 : {stopwatch.ElapsedMilliseconds} ms";
                開始執行CommandVisibility = Visibility.Visible;
            });
        }

        void GetThreadPoolConfiguration()
        {
            int ioThreads, minIoThreads, maxIoThreads, workerThreads, minWorkerThreads, maxWorkerThreads;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxIoThreads);
            ThreadPool.GetMinThreads(out minWorkerThreads, out minIoThreads);
            ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);

            AvailableWorkerThreads = workerThreads;
            AvailableIopcThreads = ioThreads;
            MinWorkerThreads = minWorkerThreads;
            MinIopcThreads = minIoThreads;
            MaxWorkerThreads = maxWorkerThreads;
            MaxIopcThreads = maxIoThreads;
        }
        public async Task<string> IssueAsynchronousRequestAsync()
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
            Thread.Sleep(IocpThreadSleep);
            return str;
        }
        public void PrintSummaryThreadCounts()
        {
            int ioThreads, minIoThreads, maxIoThreads, workerThreads, minWorkerThreads, maxWorkerThreads,
                threadCount, processThreadCount;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxIoThreads);
            ThreadPool.GetMinThreads(out minWorkerThreads, out minIoThreads);
            ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            threadCount = ThreadPool.ThreadCount;
            processThreadCount = Process.GetCurrentProcess().Threads.Count;

            TotalUseWorkerThreads = maxWorkerThreads - workerThreads;
            TotalUseIocpThreads = maxIoThreads - ioThreads;
            TotalProcessUseThreads = processThreadCount;
            TotalThreadPoolUseThreads = threadCount;

            #region 輸出執行緒使用情況
            Console.WriteLine($"Worker Threads : {TotalUseWorkerThreads}");
            Console.WriteLine($"IO Threads : {TotalUseIocpThreads}");
            Console.WriteLine($"Total Process Used Threads : {TotalProcessUseThreads}");
            Console.WriteLine($"Total ThreadPool Used Threads : {TotalThreadPoolUseThreads}");
            Console.WriteLine();
            #endregion

        }
    }
}
