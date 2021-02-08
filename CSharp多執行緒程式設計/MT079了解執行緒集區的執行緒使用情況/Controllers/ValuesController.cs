using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MT079了解執行緒集區的執行緒使用情況.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Random random = new Random();
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<string> Get()
        {
            var str = ($"Get 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = FirstAsync();
            str = str + Environment.NewLine + (await task);
            str = str + Environment.NewLine +
                ($"Get 2 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }

        async Task<string> FirstAsync()
        {
            var str = ($"FirstAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = SecondAsync();
            str = str + Environment.NewLine + (await task);
            await Task.Delay(random.Next(10, 500));
            str = str + Environment.NewLine +
                ($"FirstAsync 【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        async Task<string> SecondAsync()
        {
            var str = ($"SecondAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = ThirdAsync();
            str = str + Environment.NewLine + (await task);
            await Task.Delay(random.Next(10, 500));
            str = str + Environment.NewLine +
                ($"SecondAsync 【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        async Task<string> ThirdAsync()
        {
            var str = ($"ThirdAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            var task = FourAsync();
            await Task.Delay(random.Next(10, 500));
            str = str + Environment.NewLine + (await task);
            str = str + Environment.NewLine +
                ($"ThirdAsync【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        async Task<string> FourAsync()
        {
            var str = ($"FourAsync 1 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(random.Next(2500, 3000));
            str = str + Environment.NewLine +
                ($"FourAsync 【2】 執行緒 ID : {Thread.CurrentThread.ManagedThreadId}");

            return str;
        }
    }
}
