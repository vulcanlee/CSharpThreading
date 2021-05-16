using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT106了解各種非同步作業中對於例外異常會產生何種情況
{
    class await工作取消例外異常
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var fooTask = Task.Run(() =>
            {
                //await Task.Delay(100);
                for (int i = 0; i < int.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();
                }
            }, token);

            var barTask = Task.Run(async () =>
            {
                Console.WriteLine("取消工作開始倒數 3 秒鐘");
                await Task.Delay(3000);
                Console.WriteLine("送出取消工作的訊號");
                cts.Cancel();
            });

            //try
            //{
            await fooTask;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"發現例外異常，此例外異常型別為 : {ex.GetType().Name}");
            //}

            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatus = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatus}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
