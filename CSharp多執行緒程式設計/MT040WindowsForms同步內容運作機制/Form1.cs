using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT040WindowsForms同步內容運作機制
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 按鈕1_Click(object sender, EventArgs e)
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

        private void 按鈕2_Click(object sender, EventArgs e)
        {
            Message.Text = "按鈕2_Click 正在執行非同步工作";
            ThreadPool.QueueUserWorkItem(sc =>
            {
                Thread.Sleep(2000);
                this.Invoke(new MethodInvoker(async () =>
                {
                    Message.Text = "按鈕2_Click 的非同步作業已經完成了";
                    // 若使用 Thread.Sleep(4000) 會造成 UI 無法即時更新上面敘述內容
                    await Task.Delay(4000);
                    Message.Text = "";
                }));
            });
        }

        private void 按鈕3_Click(object sender, EventArgs e)
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
