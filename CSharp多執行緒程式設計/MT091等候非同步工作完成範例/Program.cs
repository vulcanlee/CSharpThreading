using System;
using System.Threading.Tasks;

namespace MT091等候非同步工作完成範例
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"主執行緒執行中");
            Task task = Task.Run(() =>
            {
                Console.WriteLine($"  非同步工作正在執行中");
                Task.Delay(3000).Wait();
                Console.WriteLine($"  非同步工作執行完成");
            });
            Console.WriteLine($"等候非同步工作完成");
            task.Wait();
            Console.WriteLine($"主執行緒執行完成");

        }
    }
}
