using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT100CancellationTokenSource_與_CancellationToken_的設計練習
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            #region 等候使用者輸入 取消 c 按鍵
            ThreadPool.QueueUserWorkItem(x =>
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.C)
                {
                    cts.Cancel();
                }
            });
            #endregion

            try
            {
                await Task.Run(() =>
                {
                    int cc = 0;
                    while (true)
                    {
                        if (cts.Token.IsCancellationRequested == true)
                            break;
                        if (cc++ % 10 == 0) Console.Write(".");
                        Thread.Sleep(100);
                    }
                }, cts.Token);
                //await MyMethodAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{Environment.NewLine}已經取消");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Environment.NewLine}發現例外異常 {ex.Message}");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        static async Task MyMethodAsync(CancellationToken token)
        {
            await Task.Run(() =>
            {
                int cc = 0;
                while (true)
                {
                    if (token.IsCancellationRequested == true)
                        break;
                    if (cc++ % 10 == 0) Console.Write(".");
                    Thread.Sleep(100);
                }
            });

        }
    }
}
