using System;
using System.Diagnostics;
using System.Threading;

namespace MT073修改_MT009使用SpinLock來計算
{
    class Program
    {
        // 依據你電腦執行速度，自行設定要跑的迴圈大小
        static int maxLoop = int.MaxValue / 2;
        static long counter;
        static SpinLock spinLock = new SpinLock();
        static void Main(string[] args)
        {
            counter = 0;
            Thread thread1 = new Thread(x => { Increment(); });
            Thread thread2 = new Thread(x => { Decrement(); });

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); thread1.Start(); thread2.Start();

            thread1.Join(); thread2.Join(); stopwatch.Stop();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counter}");
            Console.WriteLine($"MT009 花費時間 {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void Decrement()
        {
            bool lockTaken = false;
            for (int i = 0; i < maxLoop; i++)
            {
                lockTaken = false;
                spinLock.Enter(ref lockTaken);
                counter--;
                if (lockTaken) spinLock.Exit();
            }
        }

        private static void Increment()
        {
            bool lockTaken = false;
            for (int i = 0; i < maxLoop; i++)
            {
                lockTaken = false;
                spinLock.Enter(ref lockTaken);
                counter++;
                if (lockTaken) spinLock.Exit();
            }
        }
    }
}
