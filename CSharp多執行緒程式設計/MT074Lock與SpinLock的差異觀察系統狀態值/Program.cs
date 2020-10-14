//#define LOCK
#define SPINLOCK
using System;
using System.Threading;

namespace MT074Lock與SpinLock的差異觀察系統狀態值
{
    class Program
    {
        static object obj = new object();
        static SpinLock spinLock = new SpinLock();
#if LOCK
        static void Main(string[] args)
        {
            Thread thread = new Thread(_ =>
            {
                Thread.Sleep(1000);
                locker();
            });
            thread.Start();
            locker();
        }

        static void locker()
        {
            lock (obj)
            {
                Thread.Sleep(60 * 1000);
            }
        }
#endif
#if SPINLOCK
        static void Main(string[] args)
        {
            Thread thread = new Thread(_ =>
            {
                Thread.Sleep(1000);
                locker();
            });
            thread.Start();
            locker();
        }

        static void locker()
        {
            bool lockTaken = false;
            spinLock.Enter(ref lockTaken);
            Thread.Sleep(60 * 1000);
            spinLock.Exit();
        }
#endif
    }
}
