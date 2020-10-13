using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MT045WebAPI同步與非同步呼叫.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteSourceController : ControllerBase
    {
        [HttpGet("AddAsync/{value1}/{value2}/{delay}")]
        public async Task<string> AddAsync(int value1, int value2, int delay)
        {
            DateTime Begin = DateTime.Now;

            #region 取得當前執行緒集區的狀態
            int workerThreadsAvailable;
            int completionPortThreadsAvailable;
            int workerThreadsMax;
            int completionPortThreadsMax;
            int workerThreadsMin;
            int completionPortThreadsMin;
            ThreadPool.GetAvailableThreads(out workerThreadsAvailable, out completionPortThreadsAvailable);
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);
            #endregion

            await Task.Delay(delay);

            int sum = value1 + value2;

            DateTime Complete = DateTime.Now;
            return $"Result:{sum} " +
                $"AW:{workerThreadsAvailable} AC:{completionPortThreadsAvailable}" +
                $" MaxW:{workerThreadsMax} MaxC:{completionPortThreadsMax}" +
                $" MinW:{workerThreadsMin} MinC:{completionPortThreadsMin} ({Begin.TimeOfDay} - {Complete.TimeOfDay})";
        }
        [HttpGet("AddSync/{value1}/{value2}/{delay}")]
        public string AddSync(int value1, int value2, int delay)
        {
            DateTime Begin = DateTime.Now;
            #region 取得當前執行緒集區的狀態
            int workerThreadsAvailable;
            int completionPortThreadsAvailable;
            int workerThreadsMax;
            int completionPortThreadsMax;
            int workerThreadsMin;
            int completionPortThreadsMin;
            ThreadPool.GetAvailableThreads(out workerThreadsAvailable, out completionPortThreadsAvailable);
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);
            #endregion

            Thread.Sleep(delay);

            int sum = value1 + value2;

            DateTime Complete = DateTime.Now;
            return $"Result:{sum} " +
                $"AW:{workerThreadsAvailable} AC:{completionPortThreadsAvailable}" +
                $" MaxW:{workerThreadsMax} MaxC:{completionPortThreadsMax}" +
                $" MinW:{workerThreadsMin} MinC:{completionPortThreadsMin} ({Begin.TimeOfDay} - {Complete.TimeOfDay})";
        }
        [HttpGet("SetThreadPool/{value1}/{value2}")]
        public string SetThreadPool(int value1, int value2)
        {
            ThreadPool.SetMinThreads(value1, value2);

            string result = "OK";
            int workerThreadsAvailable;
            int completionPortThreadsAvailable;
            int workerThreadsMax;
            int completionPortThreadsMax;
            int workerThreadsMin;
            int completionPortThreadsMin;
            ThreadPool.GetAvailableThreads(out workerThreadsAvailable, out completionPortThreadsAvailable);
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);

            DateTime Complete = DateTime.Now;
            result = "OK " + $"AW:{workerThreadsAvailable} AC:{completionPortThreadsAvailable}" +
                $" MaxW:{workerThreadsMax} MaxC:{completionPortThreadsMax}" +
                $" MinW:{workerThreadsMin} MinC:{completionPortThreadsMin} ";

            return result;
        }
        [HttpGet("SetMaxThreadPool/{value1}/{value2}")]
        public string SetMaxThreadPool(int value1, int value2)
        {
            ThreadPool.SetMaxThreads(value1, value2);

            string result = "OK";
            int workerThreadsAvailable;
            int completionPortThreadsAvailable;
            int workerThreadsMax;
            int completionPortThreadsMax;
            int workerThreadsMin;
            int completionPortThreadsMin;
            ThreadPool.GetAvailableThreads(out workerThreadsAvailable, out completionPortThreadsAvailable);
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);

            DateTime Complete = DateTime.Now;
            result = "OK " + $"AW:{workerThreadsAvailable} AC:{completionPortThreadsAvailable}" +
                $" MaxW:{workerThreadsMax} MaxC:{completionPortThreadsMax}" +
                $" MinW:{workerThreadsMin} MinC:{completionPortThreadsMin} ";

            return result;
        }
        [HttpGet("GetThreadPool")]
        public string GetThreadPool()
        {
            string result = "";
            int workerThreadsAvailable;
            int completionPortThreadsAvailable;
            int workerThreadsMax;
            int completionPortThreadsMax;
            int workerThreadsMin;
            int completionPortThreadsMin;
            ThreadPool.GetAvailableThreads(out workerThreadsAvailable, out completionPortThreadsAvailable);
            ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
            ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);

            DateTime Complete = DateTime.Now;
            result = " " + $"AW:{workerThreadsAvailable} AC:{completionPortThreadsAvailable}" +
                $" MaxW:{workerThreadsMax} MaxC:{completionPortThreadsMax}" +
                $" MinW:{workerThreadsMin} MinC:{completionPortThreadsMin} ";

            return result;
        }
    }
}
