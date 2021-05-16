using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MT103Console用HttpClient從下載大檔案_並更新下載進度
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://vulcanfiles.blob.core.windows.net/$web/BlazorVideo.mp4";
            CancellationTokenSource cts = new CancellationTokenSource();

            var progress = new Progress<int>((s) =>
            {
                Console.WriteLine($"下載進度為 {s}%");
            });

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
                await 下載檔案內容(url, cts.Token, progress);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"使用者取消檔案下載");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static async Task 下載檔案內容(string page, CancellationToken token, IProgress<int> progress)
        {
            #region 使用 HttpClient 與 TAP 模式來設計
            await Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page, HttpCompletionOption.ResponseHeadersRead, token))
                    {
                        long? contentLength = response.Content.Headers.ContentLength;
                        using (HttpContent content = response.Content)
                        {
                            using (var httpStream = await content.ReadAsStreamAsync())
                            {
                                long 串流總共長度 = contentLength.Value;
                                long 串流已讀取長度 = 0;
                                long 每次讀取區塊大小 = 1024 * 30L;
                                byte[] buffer = new byte[每次讀取區塊大小];
                                while (true)
                                {
                                    int read = await httpStream.ReadAsync(buffer, 0, buffer.Length, token);
                                    if (read <= 0)
                                        break;
                                    串流已讀取長度 += read;
                                    if (progress != null)
                                    {
                                        progress.Report((int)(串流已讀取長度 * 100 / 串流總共長度));
                                    }
                                }
                            }
                        }
                    }
                }
            });
            #endregion
            return;
        }
    }
}
