using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MT048在GUI開發框架下使用BackgroundWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker foo背景工作物件 = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
        }

        void 背景工作初始化()
        {
            #region 背景工作的定義

            #region 定義背景工作開始執行的後，要做哪些事情
            foo背景工作物件.DoWork += (s, e) =>
            {
                BackgroundWorker 背景工作物件 = s as BackgroundWorker;
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine("背景工作正在執行中 (現在執行緒ID {0})...", Thread.CurrentThread.ManagedThreadId);
                    // 回報背景工作的處理進度
                    背景工作物件.ReportProgress(i * 10);
                    Thread.Sleep(500);
                }
            };
            #endregion

            #region 定義背景工作期間，當有進度回報的時候，該做甚麼處理
            foo背景工作物件.ProgressChanged += (s, e) =>
            {
                var fooMsg = string.Format("BackgroundWorker 處理進度回報:{1}%  (現在執行緒ID {0})...", Thread.CurrentThread.ManagedThreadId, e.ProgressPercentage);
                Console.WriteLine(fooMsg);
                tbkMessage.Text = fooMsg;
            };
            #endregion

            #region 定義背景工作結束的時候，要做哪些事情
            foo背景工作物件.RunWorkerCompleted += (s, e) =>
            {
                var fooMsg = string.Format("BackgroundWorker 執行完成(現在執行緒ID {0})...", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(fooMsg);
                tbkMessage.Text = fooMsg;
            };
            #endregion
            #endregion
        }
        private void btn開始_Click(object sender, RoutedEventArgs e)
        {
            背景工作初始化();
            Console.WriteLine("主要執行緒ID {0}...", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("啟動背景工作", Thread.CurrentThread.ManagedThreadId);
            foo背景工作物件.WorkerReportsProgress = true;
            foo背景工作物件.RunWorkerAsync();
        }
    }
}

