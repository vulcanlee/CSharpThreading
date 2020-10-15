using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Threading;

namespace MT075同步處理的效能測試
{
    [SimpleJob(RuntimeMoniker.CoreRt31, baseline: true)]
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SynchronizationBenchmark>();
        }
    }
    public class SynchronizationBenchmark
    {
        static int LockCounter = 0;
        static int MonitorCounter = 0;
        static int SpinLockCounter = 0;
        static int MutexLockCounter = 0;
        static int ManualResetEventCounter = 0;
        static int AutoResetEventCounter = 0;
        static int BarrierCounter = 0;
        static int CountdownEventCounter = 0;
        static int SemaphoreCounter = 0;
        static int InterlockedCounter = 0;
        object lockObject = new object();
        object monitorkObject = new object();
        object mutexObject = new object();
        SpinLock spinLock = new SpinLock();
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        Barrier barrier = new Barrier(1);
        CountdownEvent countdownEvent = new CountdownEvent(1);
        Semaphore semaphore = new Semaphore(0, 1);
        Mutex mutex = new Mutex();
        [Benchmark]
        public void LockStatment()
        {
            lock (lockObject)
            {
                LockCounter++;
            }
        }
        [Benchmark]
        public void MonitorLock()
        {
            Monitor.Enter(monitorkObject);
            MonitorCounter++;
            Monitor.Exit(monitorkObject);
        }
        [Benchmark]
        public void SpinLock()
        {
            bool lockTaken = false;
            spinLock.Enter(ref lockTaken);
            SpinLockCounter++;
            spinLock.Exit();
        }
        [Benchmark]
        public void MutexLock()
        {
            mutex.WaitOne();
            MutexLockCounter++;
            mutex.ReleaseMutex();
        }
        [Benchmark]
        public void ManualResetEventSignal()
        {
            manualResetEvent.Set();
            manualResetEvent.WaitOne();
            ManualResetEventCounter++;
            manualResetEvent.Reset();
        }
        [Benchmark]
        public void AutoResetEventSignal()
        {
            autoResetEvent.Set();
            autoResetEvent.WaitOne();
            AutoResetEventCounter++;
        }
        [Benchmark]
        public void BarrierSignal()
        {
            barrier.SignalAndWait();
            BarrierCounter++;
        }
        [Benchmark]
        public void CountdownEventSignal()
        {
            countdownEvent = new CountdownEvent(1);
            countdownEvent.Signal();
            CountdownEventCounter++;
            countdownEvent.Wait();
        }
        [Benchmark]
        public void SemaphoreNoLock()
        {
            semaphore.Release();
            SemaphoreCounter++;
            semaphore.WaitOne();
        }
        [Benchmark]
        public void InterlockedNoBlock()
        {
            Interlocked.Increment(ref InterlockedCounter);
        }
    }
}
