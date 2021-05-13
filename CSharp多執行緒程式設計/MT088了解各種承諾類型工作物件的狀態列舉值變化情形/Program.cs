using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT088了解各種承諾類型工作物件的狀態列舉值變化情形
{
    class Program
    {
        static bool IsBegin = false;
        static void Main(string[] args)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Task monitorTask = tcs.Task;
            監視工作狀態是否已經有變更(monitorTask);
            IsBegin = true;

            Thread.Sleep(1000);
            //tcs.SetResult(null);
            tcs.SetCanceled();
            //tcs.SetException(new Exception("喔喔，發生例外異常"));

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

        }

        private static void 監視工作狀態是否已經有變更(Task monitorTask)
        {
            #region 若狀態有變更，並且顯示出最新的狀態值
            string lastStatus = "";
            new Thread(() =>
            {
                while (true)
                {
                    if (IsBegin == false) return;
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
            return;
        }
    }
}
