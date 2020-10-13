using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT025建立Thread物件NET1作法
{
    class Program
    {
        static void Main(string[] args)
        {
            // ThreadStart 委派，代表在這個執行緒開始執行時要叫用的方法
            Thread thread1 = new Thread(new ThreadStart(M1));
            // 在 .NET Framework 2.0 之後，可以簡化使用底下敘述
            Thread thread10 = new Thread(M1);
            // ParameterizedThreadStart 委派，代表在這個執行緒開始執行時要叫用的方法
            Thread thread2 = new Thread(new ParameterizedThreadStart(M2));
            // 在 .NET Framework 2.0 之後，可以簡化使用底下敘述
            Thread thread20 = new Thread(M2);

            Console.WriteLine("將要休息五秒鐘，" +
                "觀察看看剛剛建立的兩個執行緒，是否有啟動");
            Thread.Sleep(5000);

            Console.WriteLine("現在要啟動執行兩個執行緒了");
            thread1.Start();
            // 要將引數傳入 ParameterizedThreadStart 委派方法內
            // 在 Start 方法中指定要傳入的引數
            thread2.Start("@");

            Thread.Sleep(3000);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void M1()
        {
            for (int i = 0; i < 500; i++)
            {
                Console.Write("*");
            }
        }
        private static void M2(object message)
        {
            for (int i = 0; i < 500; i++)
            {
                Console.Write(message.ToString());
            }
        }
    }
}
