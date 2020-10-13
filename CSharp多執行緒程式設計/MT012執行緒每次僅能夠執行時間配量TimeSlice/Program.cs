using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT012執行緒每次僅能夠執行時間配量TimeSlice
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                for (int i = 0; i < 100; i++)
                { Console.Write("*"); }
            });
            ThreadPool.QueueUserWorkItem(x =>
            {
                for (int i = 0; i < 100; i++)
                { Console.Write("-"); }
            });
            Thread.Sleep(3000);
        }
    }
}
