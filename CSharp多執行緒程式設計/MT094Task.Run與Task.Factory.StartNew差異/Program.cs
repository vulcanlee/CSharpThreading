using System;
using System.Threading.Tasks;

namespace MT094Task.Run與Task.Factory.StartNew差異
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 使用 Task.Run
            var task1 = Task.Run<string>(() =>
            {
                Task.Delay(3000).Wait();
                Console.WriteLine("非同步工作1執行完畢");
                return "非同步工作1";
            });
            task1.Wait();
            #endregion

            #region 使用 Task.Run
            var task2 = Task.Run(() =>
            {
                Task.Delay(3000).Wait();
                Console.WriteLine("非同步工作2執行完畢");
                return "非同步工作2";
            });
            task2.Wait();
            #endregion

            #region 使用 Task.Run
            var task3 = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("非同步工作3執行完畢");
                return "非同步工作3";
            });
            task3.Wait();
            #endregion

            #region 使用 Task.Factory.StartNew
            var task4 = Task.Factory.StartNew<string>(() =>
            {
                Task.Delay(3000).Wait();
                Console.WriteLine("非同步工作4執行完畢");
                return "非同步工作4";
            });
            task4.Wait();
            #endregion

            #region 使用 Task.Factory.StartNew
            var task5 = Task.Factory.StartNew(() =>
            {
                Task.Delay(3000).Wait();
                Console.WriteLine("非同步工作5執行完畢");
                return "非同步工作5";
            });
            task5.Wait();
            #endregion

            #region 使用 Task.Factory.StartNew
            var task6 = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("非同步工作6執行完畢");
                return "非同步工作6";
            });
            task6.Wait();
            #endregion

            #region 使用 Task.Factory.StartNew
            //// 解開底下註解，試圖找出問題在哪裡
            //var task7 = Task.Factory.StartNew<string>(async () =>
            //{
            //    await Task.Delay(3000);
            //    Console.WriteLine("非同步工作7執行完畢");
            //    return "非同步工作7";
            //});
            //task7.Wait();
            #endregion


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
