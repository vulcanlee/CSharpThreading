using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MT104WPF用HttpClient從下載大檔案_並更新下載進度
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        // 宣告 CancellationTokenSource ，可以用來發出取消的請求
        CancellationTokenSource cts;
        public MainWindow()
        {
            InitializeComponent();
            控制項初始化();
        }

        public void 控制項初始化()
        {
            btn開始.IsEnabled = true;
            btn取消.IsEnabled = false;
            probar進度棒.Value = 0;
        }

        public void 開始下載()
        {
            btn開始.IsEnabled = false;
            btn取消.IsEnabled = true;
            probar進度棒.Value = 0;
        }

        public void 取消下載()
        {
            btn開始.IsEnabled = true;
            btn取消.IsEnabled = false;
            probar進度棒.Value = 0;
        }

        private async void btn開始_Click(object sender, RoutedEventArgs e)
        {
            string page = "https://vulcanfiles.blob.core.windows.net/$web/BlazorVideo.mp4";
            cts = new CancellationTokenSource();
            var progressIndicator = new Progress<int>((s) =>
            {
                probar進度棒.Value = s;
            });

            開始下載();
            try
            {
                await 下載檔案內容(page, cts.Token, progressIndicator);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"使用者取消檔案下載");
            }
        }

        private async void btn取消_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            await Task.Delay(500);
            取消下載();
        }

        public Task 下載檔案內容(string page, CancellationToken token, IProgress<int> progress)
        {
            #region 使用 HttpClient 與 TAP 模式來設計
            return Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page, HttpCompletionOption.ResponseHeadersRead, cts.Token))
                    {
                        long? contentLength = response.Content.Headers.ContentLength;
                        using (HttpContent content = response.Content)
                        {
                            using (var httpStream = await content.ReadAsStreamAsync())
                            {
                                long 串流總共長度 = contentLength.Value;
                                long 串流已讀取長度 = 0;
                                byte[] buffer = new byte[1024];
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
        }
    }
}
