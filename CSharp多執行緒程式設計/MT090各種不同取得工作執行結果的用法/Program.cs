using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT090各種不同取得工作執行結果的用法
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region 使用 TPL
            // 建立一個工作，該工作會回傳 42
            Task<int> task1 = Task.Run(() =>
            {
                Console.WriteLine("第一個工作開始執行，預計要花費 3 秒鐘");
                Thread.Sleep(3000);
                return 42;
            }).ContinueWith((t) =>
            {
                // 當工作完成後，繼續執行相關運算，並且將運作值回傳，作為最終工作的回傳值
                return t.Result * 2;
            });
            task1.Wait(); // 這裡不使用封鎖等待，會有什麼問題
            Console.WriteLine(task1.Result);
            #endregion

            #region 使用 await
            // 建立一個工作，該工作會回傳 42
            Task<int> task2 = Task.Run(() =>
            {
                Console.WriteLine("第一個工作開始執行，預計要花費 2 秒鐘");
                Thread.Sleep(2000);
                return 168;
            });
            int foo最後結果 = await task2;
            foo最後結果 *= 2;
            Console.WriteLine(foo最後結果);
            #endregion

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
