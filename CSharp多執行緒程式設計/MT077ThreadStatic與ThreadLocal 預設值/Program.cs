using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT077ThreadStatic與ThreadLocal_預設值
{
    class MyThreadStaticClass
    {
        [ThreadStatic]
        public static int MyInt = 1;
    }
    class MyThreadLocalcClass
    {
        public static ThreadLocal<int> MyInt =
            new ThreadLocal<int>(() => 2);
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> allTasks = new List<Task>();
            Console.WriteLine("多執行使用 ThreadStatic 執行結果");
            for (int i = 0; i < 500; i++)
            {
                Task task = Task.Run(() =>
                {
                    Console.Write($"{MyThreadStaticClass.MyInt} ");
                });
                allTasks.Add(task);
            }
            Task.WaitAll(allTasks.ToArray());
            Console.WriteLine();

            Console.WriteLine("多執行使用 ThreadLocal 執行結果");
            allTasks.Clear();
            for (int i = 0; i < 500; i++)
            {
                Task task = Task.Run(() =>
                {
                    Console.Write($"{MyThreadLocalcClass.MyInt} ");
                });
                allTasks.Add(task);
            }
            Task.WaitAll(allTasks.ToArray());
            Console.WriteLine();
        }
    }
}
