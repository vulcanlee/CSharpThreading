using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT001使用單一執行緒同步執行遞增與遞減
{
    class Program
    {
        static Random random = new Random();
        // 依據你電腦執行速度，自行設定要跑的迴圈大小
        static int maxLoop = int.MaxValue / 2;
        static long counter;
        static void Main(string[] args)
        {
            counter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Increment();
            Console.WriteLine($"執行遞增方法後，計數器的值為 {counter}");
            Decrement();
            stopwatch.Stop();

            Console.WriteLine($"執行遞減方法後，計數器的值為 {counter}");
            Console.WriteLine($"MT001 花費時間 {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void Decrement()
        {
            for (int i = 0; i < maxLoop; i++)
            {
                counter--;
            }
        }

        private static void Increment()
        {
            for (int i = 0; i < maxLoop; i++)
            {
                counter++;
            }
        }
    }
}
