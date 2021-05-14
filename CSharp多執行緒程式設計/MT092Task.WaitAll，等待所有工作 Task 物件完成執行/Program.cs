﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT092Task.WaitAll_等待所有工作_Task_物件完成執行
{
    /// <summary>
    /// 這個範例展示了：等候所有提供的 Task 物件完成執行
    /// 我們先啟動了3個非同步工作，接下來會等候，直到所有的工作都執行完成，並且將工作的執行結果輸出到Console上
    /// </summary>
    class Program
    {
        public static void Main()
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

            Console.WriteLine($"呼叫 Task.WaitAll 前的執行緒 : {Thread.CurrentThread.ManagedThreadId}");
            Task.WaitAll(tasks); // 此處會造成現在執行緒被鎖定(Block)，直到所有的工作都完成(也許是失敗、取消)
            Console.WriteLine($"呼叫 Task.WaitAll 後的執行緒 : {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine($"結束執行三個非同步工作:{DateTime.Now}");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("  工作{0} 執行結果:{1}", i + 1, tasks[i].Result);
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
