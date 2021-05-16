using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MT106了解各種非同步作業中對於例外異常會產生何種情況
{
    class 工作Wait取消例外異常
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var fooTask = Task.Run(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();
                }
            });

            var barTask = Task.Run(async () =>
            {
                Console.WriteLine("取消工作開始倒數 1 秒鐘");
                await Task.Delay(1000);
                Console.WriteLine("送出取消工作的訊號");
                cts.Cancel();
            });

            //try
            //{
            fooTask.Wait();
            //}
            //catch (AggregateException ex)
            //{
            //    Console.WriteLine($"Status : {fooTask.Status}");
            //    Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            //    Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            //    Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            //    var exceptionStatus = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            //    Console.WriteLine($"Exception : {exceptionStatus}");
            //    exceptionStatus = (ex.InnerExceptions == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            //    Console.WriteLine($"Wait() Exception : {exceptionStatus}");
            //}

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
