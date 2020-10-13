using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT026建立Thread物件NET2作法
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(delegate ()
            {
                for (int i = 0; i < 500; i++)
                {
                    Console.Write("*");
                }
            });
            Thread thread2 = new Thread(delegate (object message)
            {
                for (int i = 0; i < 500; i++)
                {
                    Console.Write(message.ToString());
                }
            });

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
    }
}
