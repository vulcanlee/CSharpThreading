using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT010共用靜態_執行個體_區域變數測試
{
    class MyCounting
    {
        public int maxLoop = int.MaxValue / 2;
        public long counterInstance;
        public static long counterStatic;

        public void DoProcessingByShareStatic()
        {
            counterStatic = 0;
            Thread thread1 = new Thread(x =>
            {
                long loop = maxLoop;
                for (int i = 0; i < loop; i++)
                {
                    counterStatic++;
                }
            });
            Thread thread2 = new Thread(x =>
            {
                long loop = maxLoop;
                for (int i = 0; i < loop; i++)
                {
                    counterStatic--;
                }
            });

            thread1.Start(); thread2.Start();

            thread2.Join();
            thread1.Join();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counterStatic}");
        }
        public void DoProcessingByShareInstance()
        {
            counterInstance = 0;
            Thread thread1 = new Thread(x =>
            {
                long loop = maxLoop;
                for (int i = 0; i < loop; i++)
                {
                    counterInstance++;
                }
            });
            Thread thread2 = new Thread(x =>
            {
                long loop = maxLoop;
                for (int i = 0; i < loop; i++)
                {
                    counterInstance--;
                }
            });

            thread1.Start(); thread2.Start();

            thread2.Join();
            thread1.Join();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counterInstance}");
        }
        public void DoProcessingByShareMethodLocal()
        {
            int counterMethodLocal = 0;
            Thread thread1 = new Thread(x =>
            {
                long loop = maxLoop;
                for (int i = 0; i < loop; i++)
                {
                    counterMethodLocal++;
                }
            });
            Thread thread2 = new Thread(x =>
            {
                long loop = maxLoop;
                for (int i = 0; i < loop; i++)
                {
                    counterMethodLocal--;
                }
            });

            thread1.Start(); thread2.Start();

            thread2.Join();
            thread1.Join();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counterMethodLocal}");
        }
        public void DoProcessingByThreadLocal()
        {
            int counterStatic = 0;
            Thread thread1 = new Thread(x =>
            {
                long loop = maxLoop;
                int local = 0;
                for (int i = 0; i < loop; i++)
                {
                    local++;
                }
                counterStatic += local;
            });
            Thread thread2 = new Thread(x =>
            {
                long loop = maxLoop;
                int local = 0;
                for (int i = 0; i < loop; i++)
                {
                    local--;
                }
                counterStatic += local;
            });

            thread1.Start(); thread2.Start();

            thread2.Join();
            thread1.Join();
            Console.WriteLine($"執行遞增方法、遞減方法後，計數器的值為 {counterStatic}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyCounting myCounting = new MyCounting();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //myCounting.DoProcessingByShareStatic();
            //myCounting.DoProcessingByShareInstance();
            //myCounting.DoProcessingByShareMethodLocal();
            myCounting.DoProcessingByThreadLocal();
            stopwatch.Stop();

            Console.WriteLine($"花費時間  {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
