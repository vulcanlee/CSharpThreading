using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MT106了解各種非同步作業中對於例外異常會產生何種情況
{
    class 執行緒的例外異常
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                // 當在執行緒內發生了例外異常，應用程式將會結束執行
                throw new InvalidProgramException($"發生了例外異常");
            });


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
