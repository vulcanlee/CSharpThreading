﻿using System;
using System.Threading;

namespace MT056獨佔鎖定lock陳述式
{
    /// <summary>
    /// 這個範例將會說明當使用獨佔鎖定 lock陳述式
    /// </summary>
    class Program
    {
        private static int counter = 0;
        private static object 共用鎖 = new object();
        public static void Main()
        {
            Thread thread1 = new Thread(DoWork1);
            Thread thread2 = new Thread(DoWork1);
            thread1.Start(); thread2.Start();

            thread1.Join(); thread2.Join();

            Console.WriteLine($"Sum : {counter}");
            Console.ReadKey();
        }

        private static void DoWork1()
        {
            for (int index = 0; index < 1000000; index++)
            {
                lock (共用鎖)
                    counter++;
            }
        }
    }
}
