using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT107使用_TaskCompletionSource_類別_建立工作
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"呼叫非同步工作(.Result) {GetThreadId}");
            var nameLength = CustomTask("Vulcan").Result;
            Console.WriteLine($"姓名長度為 {nameLength}   {GetThreadId}");

            try
            {
                Console.WriteLine($"呼叫非同步工作(Wait()) {GetThreadId}");
                CustomTask("").Wait();
                Console.WriteLine($"呼叫非同步工作(await) {GetThreadId}");
                await CustomTask("");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生例為異常 {ex.Message}  {GetThreadId}");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static Task<int> CustomTask(string name)
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            if (string.IsNullOrEmpty(name))
            {
                tcs.SetException(new Exception("姓名欄位沒有傳入值"));
                return tcs.Task;
            }

            Task.Run(async () =>
            {
                Console.WriteLine($"   非同步工作開始執行  {GetThreadId}");
                await Task.Delay(1000);
                Console.WriteLine($"   非同步工作執行完畢  {GetThreadId}");
                tcs.SetResult(name.Length);
            });
            return tcs.Task;
        }

        static int GetThreadId => Thread.CurrentThread.ManagedThreadId;
    }
}
