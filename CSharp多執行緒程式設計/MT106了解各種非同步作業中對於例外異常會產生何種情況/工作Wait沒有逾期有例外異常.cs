using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MT106了解各種非同步作業中對於例外異常會產生何種情況
{
    class 工作Wait沒有逾期有例外異常
    {
        static void Main(string[] args)
        {
            var fooTask = Task.Run(async () =>
            {
                await Task.Delay(1500);
                throw new InvalidProgramException("發生了例外異常");
            });

            var result = fooTask.Wait(3000);

            Console.WriteLine($"此次執行的逾期狀態為 {!result}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
