﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT004修正每次遞增與遞減要鎖定而變慢問題_使用區域變數且最後才同步
{
    class Program
    {
        // 依據你電腦執行速度，自行設定要跑的迴圈大小
        static int maxLoop = int.MaxValue / 2;
        static long counter;
        static object sync = new object();
        static void Main(string[] args)
        {
            counter = 0;
            Thread thread1 = new Thread(x =>
            {
                Increment();
            });
            Thread thread2 = new Thread(x =>
            {
                Decrement();
            });

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            thread1.Start(); thread2.Start();

            thread1.Join();
            thread2.Join();
            stopwatch.Stop();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counter}");
            Console.WriteLine($"MT004 花費時間 {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void Decrement()
        {
            int localCounter = 0;
            for (int i = 0; i < maxLoop; i++)
            {
                localCounter--;
            }
            lock (sync)
            {
                counter += localCounter;
            }
        }

        private static void Increment()
        {
            int localCounter = 0;
            for (int i = 0; i < maxLoop; i++)
            {
                localCounter++;
            }
            lock(sync)
            {
                counter += localCounter;
            }
        }
    }
}
