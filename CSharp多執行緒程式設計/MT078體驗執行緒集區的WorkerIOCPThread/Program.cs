//#define HttpClientAwait
//#define HttpClientWait
//#define ReadAllTextAsync
//#define FileOpen
//#define FileStream
//#define TaskDelay
#define EntityFrameworkCore

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MT078體驗執行緒集區的WorkerIOCPThread
{
    class Program
    {
        static int loopCount = 8;
        static int workThreadSleep = 0;
        static int ioThreadSleep = 5000;
        static int monitorThreadUsageSleep = 1000;

        static void Main(string[] args)
        {
            //SetThreadPoolConfiguration();
            PrintThreadPoolConfiguration();
            PrintSummaryThreadCounts();

#if EntityFrameworkCore
            #region 適用於 Code First ，刪除資料庫與移除資料庫
            AsyncDBContext context = new AsyncDBContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            #endregion
            PrintSummaryThreadCounts();
#endif

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
#if HttpClientAwait
            // https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,6b7ffca539f4a368
            var str = await new HttpClient().GetStringAsync("https://contososyncfusion.azurewebsites.net/");
#elif HttpClientWait
            var str = new HttpClient().GetStringAsync("https://contososyncfusion.azurewebsites.net/").Result;
#elif ReadAllTextAsync
            // https://source.dot.net/#System.IO.FileSystem/System/IO/File.cs,321993cd4729625c
            var str = await File.ReadAllTextAsync("MT078體驗執行緒集區的WorkerIOCPThread.deps.json");
#elif FileOpen
            string filename = @"MT078體驗執行緒集區的WorkerIOCPThread.deps.json";
            byte[] result;
            using (FileStream SourceStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }
            var str = System.Text.Encoding.ASCII.GetString(result);
#elif FileStream
            // https://source.dot.net/#System.Private.CoreLib/FileStream.cs,2c203718e302b739
            byte[] result;
            using (FileStream SourceStream = new FileStream("MT078體驗執行緒集區的WorkerIOCPThread.deps.json",
                FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }
            var str = System.Text.Encoding.ASCII.GetString(result);
#elif EntityFrameworkCore
            string str = "Async Result";
            AsyncDBContext context = new AsyncDBContext();
            List<MyUser> myUsers = new List<MyUser>();
            for (int i = 0; i < 10; i++)
            {
                myUsers.Add(new MyUser()
                {
                    Account = $"Account{Thread.CurrentThread.ManagedThreadId}-{i}",
                    Name = $"Name{Thread.CurrentThread.ManagedThreadId}-{i}",
                    Password = $"Password{Thread.CurrentThread.ManagedThreadId}-{i}",
                });
            }
            try
            {
                context.AddRange(myUsers);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#elif TaskDelay
            string str = "Async Result";
            await Task.Delay(500);
#endif

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
    public partial class AsyncDBContext : DbContext
    {
        public AsyncDBContext()
        {
        }

        public AsyncDBContext(DbContextOptions<AsyncDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MyUser> MyUser { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AsyncDBTest");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    public class MyUser
    {
        public MyUser()
        {
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
