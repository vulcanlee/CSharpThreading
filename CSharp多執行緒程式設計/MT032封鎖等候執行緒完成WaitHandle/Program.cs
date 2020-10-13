using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT032封鎖等候執行緒完成WaitHandle
{
    /// <summary>
    /// 在這個範例中，在主執行緒使用 WaitHandle 類別的靜態 WaitAny 和 WaitAll 方法等候工作完成時，兩個執行緒要執行背景工作的方式。
    /// 這個類別一般做為同步物件 (Synchronization Object) 的基底類別 (Base Class) 使用。 衍生自 WaitHandle 的類別會定義一項信號機制，
    /// 表示取得或者釋出共用資源的存取權，但在等候存取共用資源的期間，則會使用繼承的 WaitHandle 方法來加以封鎖。
    /// </summary>
    class Program
    {
        // 定義陣列，儲存了兩個 two AutoResetEvent WaitHandles.
        static WaitHandle[] 事件等候控制代碼 = new WaitHandle[]
        {
        new AutoResetEvent(false),
        new AutoResetEvent(false)
        };

        // 定義隨機亂數用於測試之用
        static Random 隨機亂數 = new Random();

        static void Main()
        {
            #region 使用 Thread 類別，產生一個新的執行緒
            Thread regularThread1 = new Thread(state =>
            {
                // 向等候的執行緒通知發生事件
                AutoResetEvent are = (AutoResetEvent)state;

                Console.WriteLine("執行緒1，執行Thread類別產生的執行緒");
                Thread.Sleep(2000);
                Console.WriteLine("執行緒1，完成");

                // 設定作業已經成功
                are.Set();
            });
            #endregion

            #region 使用 Thread 類別，產生一個新的執行緒
            Thread regularThread2 = new Thread(state =>
            {
                // 向等候的執行緒通知發生事件
                AutoResetEvent are = (AutoResetEvent)state;

                Console.WriteLine("執行緒2，執行Thread類別產生的執行緒");
                Thread.Sleep(5000);
                Console.WriteLine("執行緒2，完成");

                // 設定作業已經成功
                are.Set();
            });
            #endregion

            // 開啟執行該執行緒
            regularThread1.Start(事件等候控制代碼[0]); regularThread2.Start(事件等候控制代碼[1]);

            WaitHandle.WaitAll(事件等候控制代碼);

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
