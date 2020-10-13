using System;
using System.Threading;

namespace MT060用Mutex限制只有一個處理程序可以執行
{
    class Program
    {
        private static string MutexName = "com.vulcanlee.process.Mutex";
        private static Mutex mutex;
        public static void Main()
        {
            if (!Mutex.TryOpenExisting(MutexName, out mutex))
            {
                mutex = new Mutex(false, MutexName);
                Console.WriteLine("該程式將會於5秒鐘後自動結束");
                Thread.Sleep(15000);
            }
            else
            {
                Console.WriteLine($"已經有相同的程序啟動了，無法再度執行");
                return;
            }
        }

    }
}
