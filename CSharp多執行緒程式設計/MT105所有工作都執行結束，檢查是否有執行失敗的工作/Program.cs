using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT105所有工作都執行結束_檢查是否有執行失敗的工作
{
    class Program
    {
        static List<int> taskIDs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static async Task Main(string[] args)
        {
            Task[] allTasks = (from TaskID in taskIDs select 非同步工作委派方法Async(TaskID)).ToArray();
            try
            {
                await Task.WhenAll(allTasks);
                //Task.WaitAll(allTasks);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"捕捉到例外異常的物件型別為 : {exc.GetType().Name}");
                // 當所有等候工作都執行結束後，可以檢查是否有執行失敗的工作
                foreach (Task faulted in allTasks.Where(t => t.IsFaulted))
                {
                    Console.WriteLine(faulted.Exception.InnerException.Message);
                }
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static async Task 非同步工作委派方法Async(int id)
        {
            await Task.Delay(2500 - id * 10);
            Console.WriteLine($"執行工作 {id}");
            if (id % 3 == 0)
            {
                throw new Exception(string.Format("發生異常了，工作ID是{0}", id));
            }
        }
    }
}
