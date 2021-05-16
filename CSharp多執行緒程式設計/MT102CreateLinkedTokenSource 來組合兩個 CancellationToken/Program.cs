using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MT102CreateLinkedTokenSource_來組合兩個_CancellationToken
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://lobworkshop.azurewebsites.net/api/RemoteSource/Add/123/321/10";

            Console.WriteLine($"若按下 c 按鍵 或者 超過 8秒鐘後，則會取消此次非同步存取");

            CancellationTokenSource ctsUser = new CancellationTokenSource();
            CancellationTokenSource ctsTimeout = new CancellationTokenSource();
            ctsTimeout.CancelAfter(8000);
            CancellationTokenSource combinationCTS = CancellationTokenSource.CreateLinkedTokenSource(ctsUser.Token, ctsTimeout.Token);

            #region 等候使用者輸入 取消 c 按鍵
            ThreadPool.QueueUserWorkItem(x =>
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.C)
                {
                    ctsUser.Cancel();
                }
            });
            #endregion

            try
            {
                string result = await AccessTheWebAsync(url, combinationCTS.Token);
                Console.WriteLine($"{Environment.NewLine}全部下載完成");
                Console.WriteLine($"非同步計算結果為:{result}");
            }
            catch (OperationCanceledException)
            {
                if (ctsTimeout.IsCancellationRequested == true)
                    Console.WriteLine($"{Environment.NewLine}超過 8 秒鐘，下載已經自動取消");
                if (ctsUser.IsCancellationRequested == true)
                    Console.WriteLine($"{Environment.NewLine}使用者按下 c 按鍵，下載已經取消");
            }
            catch (Exception)
            {
                Console.WriteLine($"{Environment.NewLine}下載發現例外異常，已經中斷");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        static async Task<string> AccessTheWebAsync(string url, CancellationToken ct)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine($"開始存取 URL : {url}");
            HttpResponseMessage response = await client.GetAsync(url, ct);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
