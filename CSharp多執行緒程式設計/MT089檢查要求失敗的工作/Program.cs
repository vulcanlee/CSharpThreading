using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MT089檢查要求失敗的工作
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> urls = new()
            {
                "https://lobworkshop.azurewebsites.net/api/RemoteSource/Source3",
                "https://lobworkshop.azurewebsites.net/api/RemoteSource/Source3",
                "https://fake.host.net/api/RemoteSource/Source3",
            };
            List<Task> allTasks = new();

            for (int i = 0; i < 3; i++)
            {
                var idx = i;
                string url = urls[i];
                var task = Task.Run(() =>
                {
                    HttpClient client = new HttpClient();
                    Console.WriteLine($"第 {idx}-1 測試開始時間 {DateTime.Now}");
                    var result = client.GetStringAsync(url).Result;
                    Console.WriteLine($"第 {idx}-1 測試結束時間 {DateTime.Now}");
                });

                allTasks.Add(task);
            }

            try
            {
                Task.WhenAll(allTasks).Wait();
            }
            catch (Exception)
            {
            }

            foreach (var item in allTasks)
            {
                Console.WriteLine($"CompletedSuccessfully:{item.IsCompletedSuccessfully}" +
                    $" Faulted:{item.IsFaulted} Canceled:{item.IsCanceled}" +
                    $" Completed:{item.IsCompleted}");
            }
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
