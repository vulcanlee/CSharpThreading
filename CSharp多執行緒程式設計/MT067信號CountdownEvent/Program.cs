﻿using System;
using System.Threading;

namespace MT067信號CountdownEvent
{
    class Program
    {
        private static CountdownEvent MySyncEvent = new CountdownEvent(4);
        static void Main(string[] args)
        {
            for (int i = 1; i <= 4; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            Console.WriteLine("等候四個執行緒執行完畢");
            MySyncEvent.Wait();
            Console.WriteLine("四個執行緒已經執行完成");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行");
            Thread.Sleep(new Random().Next(1000,5000));
            Console.WriteLine($"@ 工作執行緒{state} 執行完成");
            MySyncEvent.Signal();
        }
    }
}
