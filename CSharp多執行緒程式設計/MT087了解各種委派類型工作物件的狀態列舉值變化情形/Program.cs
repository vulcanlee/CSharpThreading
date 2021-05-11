using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT087了解各種委派類型工作物件的狀態列舉值變化情形
{
    class Program
    {
        static void Main(string[] args)
        {
            string lastStatus = "";
            Task monitorTask;
            bool IsBegin = false;

            monitorTask = new Task(() =>
            {
                Thread.Sleep(3000);
                throw new Exception("喔喔，發生例外異常");
            });

            lastStatus = 監視工作狀態是否已經有變更(lastStatus, monitorTask);
            Thread.Sleep(300);
            monitorTask.Start();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static string 監視工作狀態是否已經有變更(string lastStatus, Task monitorTask)
        {
            #region 若狀態有變更，並且顯示出最新的狀態值
            new Thread(() =>
            {
                while (true)
                {
                    var tmpStatus = monitorTask.Status;
                    if (tmpStatus.ToString() != lastStatus)
                    {
                        Console.WriteLine($"Status : {monitorTask.Status}");
                        Console.WriteLine($"IsCompleted : {monitorTask.IsCompleted}");
                        Console.WriteLine($"IsCanceled : {monitorTask.IsCanceled}");
                        Console.WriteLine($"IsFaulted : {monitorTask.IsFaulted}");
                        var exceptionStatusX = (monitorTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
                        Console.WriteLine($"Exception : {exceptionStatusX}");
                        Console.WriteLine();
                        lastStatus = tmpStatus.ToString();
                    }
                }
            })
            { IsBackground = false }.Start();

            #endregion
            return lastStatus;
        }
    }
}
