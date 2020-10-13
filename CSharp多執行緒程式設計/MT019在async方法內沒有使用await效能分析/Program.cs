//#define 同步方法
#define 空的非同步方法
//#define 立即返回非同步方法

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT019在async方法內沒有使用await效能分析
{
    class Program
    {
#if 同步方法
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                string foo = 同步方法();
            }
            sw.Stop();
            double cost = sw.Elapsed.TotalMilliseconds / 1000.0;
            Console.WriteLine($"呼叫 1000 次 同步方法 的平均花費時間 : {cost} ms");
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static string 同步方法()
        {
            return "同步方法";
        }
#elif 空的非同步方法
        static async Task Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                string foo = await 空的非同步方法();
            }
            sw.Stop();
            double cost = sw.Elapsed.TotalMilliseconds / 1000.0;
            Console.WriteLine($"呼叫 1000 次 同步方法 的平均花費時間 : {cost} ms");
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static async Task<string> 空的非同步方法()
        {
            return "同步方法";
        }
#elif 立即返回非同步方法
        static async Task Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                string foo = await 立即返回非同步方法();
            }
            sw.Stop();
            double cost = sw.Elapsed.TotalMilliseconds / 1000.0;
            Console.WriteLine($"呼叫 1000 次 同步方法 的平均花費時間 : {cost} ms");
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static async Task<string> 立即返回非同步方法()
        {
            await Task.Yield();
            return "同步方法";
        }
#endif
    }
}
