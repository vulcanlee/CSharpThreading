using System;
using System.Collections.Generic;
using System.Threading;

namespace MT108客製化SynchronizationContext
{
    class Program
    {
        static MySynchronizationContext ctx = new MySynchronizationContext();
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Main Thread No {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"設定自製 SynchronizationContext 之前的 SynchronizationContext.Current : {SynchronizationContext.Current?.ToString()}");

            // 設定我們自己設計的 同步處理的內容 synchronisation context
            MySynchronizationContext.SetSynchronizationContext(ctx);

            Console.WriteLine($"設定自製 SynchronizationContext 之後的 SynchronizationContext.Current : {SynchronizationContext.Current?.ToString()}");

            Thread workerThread = new Thread(new ThreadStart(Run));
            workerThread.Start();

            Thread againThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    var foo = Console.ReadKey();
                    if (foo.KeyChar == 'a')
                    {
                        Run();
                    }
                    else if (foo.KeyChar == 'q')
                    {
                        ctx.Cancel();
                        break;
                    };
                }
            }));
            againThread.Start();

            //  開始進行 訊息處理的無窮迴圈
            ctx.RunMessagePump();
        }

        private static void Run()
        {
            Console.Out.WriteLine("Current New Thread No {0}", Thread.CurrentThread.ManagedThreadId);

            // 將要處理的委派方法項目，送入 訊息處理的無窮迴圈 執行
            ctx.Post(RunDownloadBlogger, null);
        }

        private static void RunDownloadBlogger(object state)
        {
            DownloadBlogger();
        }

        static async void DownloadBlogger()
        {
            Console.Out.WriteLine("MainProgram 下載前 on Thread No {0}", Thread.CurrentThread.ManagedThreadId);
            var client = new System.Net.WebClient();
            var webContentHomePage = await client.DownloadStringTaskAsync("https://mylabtw.blogspot.com/");
            Console.Out.WriteLine("下載 {0} 字元", webContentHomePage.Length);
            Console.Out.WriteLine("MainProgram 下載完後 on Thread No {0} ", Thread.CurrentThread.ManagedThreadId);
        }
    }

    class MySynchronizationContext : SynchronizationContext
    {
        /// <summary>
        /// 待執行的訊息工作佇列
        /// </summary>
        private readonly Queue<Action> messagesQueue = new Queue<Action>();
        /// <summary>
        /// 用於同步處理之鎖定的物件
        /// </summary>
        private readonly object syncHandle = new object();
        /// <summary>
        /// 是否正在執行中
        /// </summary>
        private bool isRunning = true;

        public override void Send(SendOrPostCallback codeToRun, object state)
        {
            throw new NotImplementedException();
        }

        public override void Post(SendOrPostCallback codeToRun, object state)
        {
            lock (syncHandle)
            {
                // 將要處理的訊息工作，加入佇列中
                messagesQueue.Enqueue(() => codeToRun(state));
                SignalContinue();
            }
        }

        /// <summary>
        /// 進入訊息處理的無窮迴圈
        /// </summary>
        public void RunMessagePump()
        {
            while (CanContinue())
            {
                Console.Write(".");
                Action nextToRun = RetriveItem();
                if (nextToRun != null)
                    nextToRun();
            }
        }

        /// <summary>
        /// 取出待處理的訊息工作項目
        /// </summary>
        /// <returns></returns>
        private Action RetriveItem()
        {
            lock (syncHandle)
            {
                while (CanContinue() && messagesQueue.Count == 0)
                {
                    Monitor.Wait(syncHandle);
                }
                if (isRunning == true)
                {
                    return messagesQueue.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 是否可以繼續執行
        /// </summary>
        /// <returns></returns>
        private bool CanContinue()
        {
            lock (syncHandle)
            {
                return isRunning;
            }
        }

        /// <summary>
        /// 停止 訊息處理的無窮迴圈 執行
        /// </summary>
        public void Cancel()
        {
            lock (syncHandle)
            {
                isRunning = false;
                SignalContinue();
            }
        }

        /// <summary>
        /// 解除鎖定，可以繼續執行
        /// </summary>
        private void SignalContinue()
        {
            Monitor.Pulse(syncHandle);
        }
    }
}
