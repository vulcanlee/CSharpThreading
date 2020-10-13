using System;
using System.Diagnostics;

namespace MT042靜態與區域變數的存取速度測試
{
    class Program
    {
        static int staticInt = 0;
        static void Main(string[] args)
        {
            int localInt = 0;
            StaticObjectAccess();
            StaticObjectAccess();
            StaticObjectAccess();
            LocalObjectAccess(localInt);
            LocalObjectAccess(localInt);
            LocalObjectAccess(localInt);
        }

        private static void LocalObjectAccess(int localInt)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (i % 2 == 0)
                    localInt++;
                else
                    localInt--;
            }
            stopwatch.Stop();
            Console.WriteLine($"Access Local : {stopwatch.ElapsedMilliseconds}ms");
            return;
        }

        private static void StaticObjectAccess()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (i % 2 == 0)
                    staticInt++;
                else
                    staticInt--;
            }
            stopwatch.Stop();
            Console.WriteLine($"Access Static : {stopwatch.ElapsedMilliseconds}ms");
            return;
        }
    }
}
