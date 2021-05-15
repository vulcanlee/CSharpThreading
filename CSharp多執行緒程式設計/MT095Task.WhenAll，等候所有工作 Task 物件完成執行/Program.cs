using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT095Task.WhenAll_等候所有工作_Task_物件完成執行
{
    class Program
    {
        public static async Task Main()
        {
            Task<int>[] tasks = new Task<int>[3];

            Console.WriteLine($"開始執行三個非同步工作:{DateTime.Now}");

            #region 建立與執行三個非同步工作
            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
                return 1;
            });
            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("2");
                return 2;
            });
            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("3");
                return 3;
            }
            );
            #endregion

            Console.WriteLine($"呼叫 前的執行緒 : {Thread.CurrentThread.ManagedThreadId}");
            //int[] tasksResult = await Task.WhenAll(tasks);
            int[] tasksResult = Task.WhenAll(tasks).Result;
            Console.WriteLine($"呼叫 後的執行緒 : {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine($"結束執行三個非同步工作:{DateTime.Now}");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("  工作{0} 執行結果:{1}", i + 1, tasksResult[i]);
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
