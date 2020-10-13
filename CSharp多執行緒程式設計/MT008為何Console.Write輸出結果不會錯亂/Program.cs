using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT008為何Console.Write輸出結果不會錯亂
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                for (int i = 0; i < 20; i++)
                { Console.Write("AAAA"); }
            });
            ThreadPool.QueueUserWorkItem(x =>
            {
                for (int i = 0; i < 20; i++)
                { Console.Write("BBBB"); }
            });
            ThreadPool.QueueUserWorkItem(x =>
            {
                for (int i = 0; i < 20; i++)
                { Console.Write("CCCC"); }
            });

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
