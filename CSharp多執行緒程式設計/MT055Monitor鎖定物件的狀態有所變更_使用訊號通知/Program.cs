using System;
using System.Threading;

namespace MT055Monitor鎖定物件的狀態有所變更_使用訊號通知
{
    /// <summary>
    /// 這個範例將會說明當使用獨佔鎖定 Monitor ，如何使用 Monitor.Wait / Monitor.PulseAll 在不同執行緒間，進行同步執行工作
    /// </summary>
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(MyMethod);
            // 這裡需要等候一段時間，讓工作執行緒已經準備與開始執行了
            Thread.Sleep(300);
            Console.WriteLine($"主執行緒 準備進入到獨佔鎖定狀態 lock (locker)");
            lock (locker)
            {
                // 因為其他執行緒執行  Monitor.Wait(locker); 所以，便可以進入此關鍵區域
                Console.WriteLine($"主執行緒 發出 PulseAll 訊號  Monitor.PulseAll(locker)");
                // 通知所有等候中的執行緒，物件的狀態有所變更。
                Monitor.PulseAll(locker);
                Console.WriteLine($"主執行緒 使用 Wait 等候訊號通知  Monitor.Wait(locker)");
                // 
                Monitor.Wait(locker);
                // 若其他執行緒發出 Monitor.PulseAll(locker); 訊號，這裡便可以繼續執行
                Console.WriteLine($"主執行緒 收到來自工作執行緒訊號通知 ");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            Console.WriteLine($"@ 工作執行緒 準備進入獨佔鎖定狀態 lock (locker)");
            // 因為主執行緒暫停300ms，因此，這個執行緒可以先獲得 鎖定 物件
            lock (locker)
            {
                while (true)
                {
                    Console.WriteLine($"@ 工作執行緒 等候 主執行緒 的訊號通知 Monitor.Wait(locker)");
                    // 釋出物件的鎖並且封鎖目前的執行緒，直到這個執行緒重新取得鎖定為止。
                    Monitor.Wait(locker);
                    // 因為主執行緒執行 Monitor.PulseAll(locker); ，所以，可以繼續執行
                    Console.WriteLine($"@ 工作執行緒 模擬需要執行");
                    Console.WriteLine($"@ 工作執行緒 發出 PulseAll 訊號 Monitor.PulseAll(locker)");
                    // 通知所有等候中的執行緒，物件的狀態有所變更。
                    Monitor.PulseAll(locker);
                }
            }
        }
    }
}
