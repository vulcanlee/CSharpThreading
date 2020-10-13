using System;
using System.Threading;

namespace MT057MonitorPulseAllPulse的差異
{
    class Program
    {
        static object locker = new object();
        static bool condition = false;
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(等候訊號通知的執行緒);
            Thread thread2 = new Thread(等候訊號通知的執行緒);
            Thread thread3 = new Thread(等候訊號通知的執行緒);
            Thread thread4 = new Thread(送出解除訊號的執行緒);
            thread1.Start("執行緒1");
            thread2.Start("執行緒2");
            thread3.Start("執行緒3");
            Thread.Sleep(3000);
            thread4.Start(2);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static void 等候訊號通知的執行緒(object obj)
        {
            Monitor.Enter(locker);
            Console.WriteLine($"{obj} 等候訊號通知以便繼續執行");
            while (!condition)
                Monitor.Wait(locker);
            Monitor.Exit(locker);
            Console.WriteLine($"{obj} 結束執行");
        }
        static void 送出解除訊號的執行緒(object obj)
        {
            int loop = Convert.ToInt32(obj);
            Monitor.Enter(locker);
            Console.WriteLine($"休息3秒後，解除{loop}個等待執行緒");
            Thread.Sleep(3000);
            condition = true;
            for (int i = 0; i < loop; i++)
            {
                // 若這裡使用 PulseAll ，所有等待執行緒都會繼續執行
                Monitor.Pulse(locker);
            }
            Monitor.Exit(locker);
            Console.WriteLine($"執行緒4 結束執行");
        }
    }
}
