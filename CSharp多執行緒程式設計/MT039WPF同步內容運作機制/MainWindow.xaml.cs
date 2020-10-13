using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MT039WPF同步內容運作機制
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void 按鈕1_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "按鈕1_Click 正在執行非同步工作";
            ThreadPool.QueueUserWorkItem(sc =>
            {
                Thread.Sleep(2000);
                ((SynchronizationContext)sc).Send(async _ =>
                {
                    Message.Text = "按鈕1_Click 的非同步作業已經完成了";
                    // 若使用 Thread.Sleep(4000) 會造成 UI 無法即時更新上面敘述內容
                    await Task.Delay(4000);
                    Message.Text = "";
                }, null);
            }, SynchronizationContext.Current);
        }

        private void 按鈕2_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "按鈕2_Click 正在執行非同步工作";
            ThreadPool.QueueUserWorkItem(sc =>
            {
                Thread.Sleep(2000);
                Dispatcher.Invoke(async () =>
                {
                    Message.Text = "按鈕2_Click 的非同步作業已經完成了";
                    // 若使用 Thread.Sleep(4000) 會造成 UI 無法即時更新上面敘述內容
                    await Task.Delay(4000);
                    Message.Text = "";
                });
            });
        }

        private void 按鈕3_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "按鈕3_Click 正在執行非同步工作";
            ThreadPool.QueueUserWorkItem(sc =>
            {
                Thread.Sleep(2000);
                Message.Text = "按鈕3_Click 的非同步作業已經完成了";
                Thread.Sleep(4000);
                Message.Text = "";
            });
        }
    }
}
