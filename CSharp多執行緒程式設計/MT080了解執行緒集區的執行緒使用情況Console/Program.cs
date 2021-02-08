using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT080了解執行緒集區的執行緒使用情況Console
{
    class Program
    {
        static Random random = new Random();
        static async Task Main(string[] args)
        {
            var str = ($"Get 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = FirstAsync();
            str = str + Environment.NewLine + (await task);
            str = str + Environment.NewLine +
                ($"Get 2 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(str);
        }
        static async Task<string> FirstAsync()
        {
            var str = ($"FirstAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = SecondAsync();
            str = str + Environment.NewLine + (await task);
            await Task.Delay(random.Next(10, 500));
            str = str + Environment.NewLine +
                ($"FirstAsync 【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        static async Task<string> SecondAsync()
        {
            var str = ($"SecondAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = ThirdAsync();
            str = str + Environment.NewLine + (await task);
            await Task.Delay(random.Next(10, 500));
            str = str + Environment.NewLine +
                ($"SecondAsync 【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        static async Task<string> ThirdAsync()
        {
            var str = ($"ThirdAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = FourAsync();
            await Task.Delay(random.Next(10, 500));
            str = str + Environment.NewLine + (await task);
            str = str + Environment.NewLine +
                ($"ThirdAsync【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        static async Task<string> FourAsync()
        {
            var str = ($"FourAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(random.Next(2500, 3000));
            str = str + Environment.NewLine +
                ($"FourAsync 【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");

            return str;
        }
    }
}
