using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT037執行緒發生例外異常
{
    /// <summary>
    /// 在這個範例中，共啟動了4個執行緒，當執行緒內的執行方法發生了例外異常，是會造成整個處理程序意外終止
    /// 其中，在執行緒2內，我們有捕捉例外異常，並且還原內容與釋放資源，因此，執行緒2，不會造成處理程序意外終止與造成處理程序不穩定
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("執行緒1發生例外異常...");
                    throw new OverflowException();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("執行緒1發生 當檢查內容中的算數、轉型 (Casting) 或轉換作業發生溢位時所擲回的例外狀況");
                }
                finally
                {
                    // 請在這裡將取得的資源釋放掉，並且還原已經處理過的程序內容
                }
            });
            thread1.Name = "執行緒1";
            Thread thread2 = new Thread(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("執行緒2發生例外異常...");
                throw new FormatException();
            });
            thread2.Name = "執行緒2";
            Thread thread3 = new Thread(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("執行緒3發生例外異常...");
                throw new Exception("自訂異常");
            });
            thread3.Name = "執行緒3";

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Thread.Sleep(5000);
            Console.WriteLine("主執行緒發生例外異常...");
            throw new NullReferenceException();

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();

        }
    }
}
