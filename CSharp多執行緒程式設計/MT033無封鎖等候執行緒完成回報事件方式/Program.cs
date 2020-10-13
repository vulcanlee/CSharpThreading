﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT033無封鎖等候執行緒完成回報事件方式
{
    // 在這個範例內，我們將會透過自訂事件類別，傳遞多個參數到執行緒內，並且當執行緒執行完成後，會自動執行我們事前設定事件方法，
    // 在完成的事件方法內，我們可以取得該執行緒的最後執行完成結果
    class Program
    {
        static void Main(string[] args)
        {
            ThreadWorker worker = new ThreadWorker();
            // 設定執行緒完成後，需要委派執行的方法
            worker.On執行緒完成 += 執行緒完成事件方法;

            // 產生一個新的執行緒，並且開始執行該執行緒
            Console.WriteLine("產生一個新的執行緒，並且開始執行該執行緒");
            worker.Processing(10,20);

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void 執行緒完成事件方法(object sender, EventArgs e)
        {
            #region 取得執行緒計算完成結果
            執行完成後回報結果Args foo執行完成後回報結果Args = e as 執行完成後回報結果Args;
            Console.WriteLine("執行緒計算結果為{0}", foo執行完成後回報結果Args.計算結果);
            #endregion
        }
    }

    class ThreadWorker
    {
        public event EventHandler On執行緒完成;
        int 傳遞的參數1;
        int 傳遞的參數2;

        public void Processing(int para1, int para2)
        {
            傳遞的參數1 = para1;
            傳遞的參數2 = para2;
            Thread thread1 = new Thread(Run);
            thread1.Start();
        }

        public void Run()
        {
            // 在這裡開始進行執行緒所要處理的工作
            int sum = 0;
            for (int i = 0; i < 傳遞的參數1; i++)
            {
                for (int j = 0; j < 傳遞的參數2; j++)
                {
                    sum += j;
                }
            }
            Thread.Sleep(2000); // 表示接下來的工作，需要花費 2 秒鐘

            if (On執行緒完成 != null)
            {
                // 定義要回報的工作結果，並且將計算結果回報回去
                執行完成後回報結果Args foo執行完成後回報結果Args = new 執行完成後回報結果Args
                {
                    計算結果 = sum,
                };
                // 執行 執行緒 執行完成後，需要委派的方法，也回報執行結果
                On執行緒完成(this, foo執行完成後回報結果Args);
            }
        }
    }

    class 執行完成後回報結果Args : EventArgs
    {
        public int 計算結果 { get; set; }
    }
}
