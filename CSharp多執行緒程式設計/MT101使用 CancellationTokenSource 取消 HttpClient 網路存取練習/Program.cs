using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MT101使用_CancellationTokenSource_取消_HttpClient_網路存取練習
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://lobworkshop.azurewebsites.net/api/RemoteSource/Add/123/321/10";

            Console.WriteLine($"若在10秒鐘內，按下 c 按鍵，則會取消此次非同步存取");

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
                string result = await AccessTheWebAsync(url, cts.Token);
                Console.WriteLine($"{Environment.NewLine}全部下載完成");
                Console.WriteLine($"非同步計算結果為:{result}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{Environment.NewLine}下載已經取消");
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
