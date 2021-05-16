using System;
using System.Threading.Tasks;

namespace MT106了解各種非同步作業中對於例外異常會產生何種情況
{
    class await工作例外異常
    {
        static async Task Main(string[] args)
        {
            var fooTask = Task.Run(async () =>
            {
                await Task.Delay(500);
                throw new InvalidProgramException("發生了例外異常");
            });

            //try
            //{
            await fooTask;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"發現例外異常，此例外異常型別為 : {ex.GetType().Name}");
            //}

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
