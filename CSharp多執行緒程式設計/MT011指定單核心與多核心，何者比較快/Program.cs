using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT011指定單核心與多核心_何者比較快
{
    class Program
    {
        private static long counter = 0;
        public static void Main()
        {
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)1;   // 00000001
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)3;   // 00000011
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)5;   // 00000101
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)21;  // 00010101
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)12;  // 00001100
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)48;  // 00110000
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)192; // 11000000
            //Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)255; // 11111111

            Thread thread1 = new Thread(MyMethod);
            Thread thread2 = new Thread(MyMethod);

            thread1.Start(); thread2.Start();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            thread1.Join(); thread2.Join();

            sw.Stop();

            Console.WriteLine($"Sum is {counter}");
            Console.WriteLine($"Total is {sw.Elapsed.TotalMilliseconds} ms");
        }

        private static void MyMethod()
        {
            var loop = int.MaxValue / 4;
            for (int index = 0; index < loop; index++)
            { Interlocked.Increment(ref counter); }
        }
    }
}
