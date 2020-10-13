//#define 沒有使用同步內容的非同步應用
//#define 使用同步內容的非同步應用
//#define 使用Wait等候非同步應用完成
#define 使用Wait等候非同步應用完成Post
//#define HttpClient使用Wait等候非同步應用完成
//#define HttpClient使用Wait等候非同步應用完成Task
//#define HttpClient使用Wait等候非同步應用完成Wait
//#define 使用await等待非同步應用完成
//#define HttpClient使用await等待非同步應用完成
//#define async方法優化的作法
//#define async方法優化的作法1

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MT041ASP.NET_MVC與同步內容的體驗.Controllers
{
    public class HomeController : Controller
    {

#if async方法優化的作法1
        public async Task<ActionResult> Index()
        {
            var myTask = MyMethodAsync(
                "https://lobworkshop.azurewebsites.net/api/" +
                "RemoteSource/Add/22/33/3");
            Debug.WriteLine($"HTTP Request 封鎖式等待非同步工作完成");
            myTask.Wait();
            Debug.WriteLine($"HTTP Request 執行完畢");

            return View();
        }
        async Task<string> MyMethodAsync(string url)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                Debug.WriteLine($"呼叫 Web API 之前");
                result = await client.GetStringAsync(url)
                    .ConfigureAwait(false);
                Debug.WriteLine($"呼叫 Web API 之後");
            }
            return result;
        }
#endif

#if async方法優化的作法
        public async Task<ActionResult> Index()
        {
            var myTask = MyMethodAsync(
                "https://lobworkshop.azurewebsites.net/api/" +
                "RemoteSource/Add/22/33/3");
            Debug.WriteLine($"HTTP Request 等待前 執行緒ID:" +
                $"{Thread.CurrentThread.ManagedThreadId} 內容同步:" +
                (SynchronizationContext.Current == null ? "null" : "存在"));
            await myTask;
            Debug.WriteLine($"HTTP Request 等待後 執行緒ID:" +
                $"{Thread.CurrentThread.ManagedThreadId} 內容同步:" +
                (SynchronizationContext.Current == null ? "null" : "存在"));

            return View();
        }
        async Task<string> MyMethodAsync(string url)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                Debug.WriteLine($"呼叫 Web API 之前");
                result = await client.GetStringAsync(url)
                    .ConfigureAwait(false);
                Debug.WriteLine($"呼叫 Web API 之後");
            }
            return result;
        }
#endif

#if HttpClient使用await等待非同步應用完成
        public async Task<ActionResult> Index()
        {
            Task<string> myTask = Sun();

            Debug.WriteLine($"HTTP Request 封鎖式等待非同步工作完成");
            await myTask;
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
        public async Task<string> Sun()
        {
            Debug.WriteLine($"呼叫 Web API 之前");
            string result = await new HttpClient().GetStringAsync(
                "https://lobworkshop.azurewebsites.net/api/" +
                "RemoteSource/Add/22/33/3");
            Debug.WriteLine($"呼叫 Web API 之後");
            return result;
        }
#endif

#if 使用await等待非同步應用完成
        public async Task<ActionResult> Index()
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            Task myTask = Task.Run(() =>
            {
                Debug.WriteLine($"背景執行緒 休息 1 秒鐘");
                Thread.Sleep(1000);
                sc.Send(_ =>
                {
                    Debug.WriteLine($"背景執行緒回到 HTTP Request 執行內容");
                }, null);
            });

            Debug.WriteLine($"HTTP Request 等待非同步工作完成");
            await myTask;
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
#endif

#if HttpClient使用Wait等候非同步應用完成Task
        public async Task<ActionResult> Index()
        {
            Debug.WriteLine($"呼叫 Web API 之前");
            Task<string> myTask = new HttpClient().GetStringAsync(
                "https://lobworkshop.azurewebsites.net/api/" +
                "RemoteSource/Add/22/33/3");
            Debug.WriteLine($"呼叫 Web API 之後");

            Debug.WriteLine($"HTTP Request 封鎖式等候非同步工作完成");
            myTask.Wait();
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
#endif

#if HttpClient使用Wait等候非同步應用完成Wait
        public async Task<ActionResult> Index()
        {
            Task<string> myTask = Sun();

            Debug.WriteLine($"HTTP Request 封鎖式等候非同步工作完成");
            myTask.Wait();
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
        public async Task<string> Sun()
        {
            Debug.WriteLine($"呼叫 Web API 之前");
            string result = new HttpClient().GetStringAsync(
            "https://lobworkshop.azurewebsites.net/api/" +
            "RemoteSource/Add/22/33/3").Result;
            Debug.WriteLine($"呼叫 Web API 之後");
            return result;
        }
#endif

#if HttpClient使用Wait等候非同步應用完成
        public async Task<ActionResult> Index()
        {
            Task<string> myTask = Sun();

            Debug.WriteLine($"HTTP Request 封鎖式等候非同步工作完成");
            myTask.Wait();
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
        public async Task<string> Sun()
        {
            Debug.WriteLine($"呼叫 Web API 之前");
            string result = await new HttpClient().GetStringAsync(
                "https://lobworkshop.azurewebsites.net/api/" +
                "RemoteSource/Add/22/33/3");
            Debug.WriteLine($"呼叫 Web API 之後");
            return result;
        }
#endif

#if 使用Wait等候非同步應用完成Post
        public async Task<ActionResult> Index()
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            Task myTask = Task.Run(() =>
            {
                Debug.WriteLine($"背景執行緒 休息 1 秒鐘");
                Thread.Sleep(1000);
                sc.Post(_ =>
                {
                    Debug.WriteLine($"背景執行緒回到 HTTP Request 執行內容");
                }, null);
            });

            Debug.WriteLine($"HTTP Request 封鎖式等候非同步工作完成");
            myTask.Wait();
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
#endif

#if 使用Wait等候非同步應用完成
        public async Task<ActionResult> Index()
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            Task myTask = Task.Run(() =>
            {
                Debug.WriteLine($"背景執行緒 休息 1 秒鐘");
                Thread.Sleep(1000);
                sc.Send(_ =>
                {
                    Debug.WriteLine($"背景執行緒回到 HTTP Request 執行內容");
                }, null);
            });

            Debug.WriteLine($"HTTP Request 封鎖式等候非同步工作完成");
            myTask.Wait();
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
#endif

#if 使用同步內容的非同步應用
        public async Task<ActionResult> Index()
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            Task myTask = Task.Run(() =>
            {
                Debug.WriteLine($"背景執行緒 休息 1 秒鐘");
                Thread.Sleep(1000);
                sc.Send(_ =>
                {
                    Debug.WriteLine($"背景執行緒回到 HTTP Request 執行內容");
                }, null);
            });

            Debug.WriteLine($"HTTP Request 休息 3 秒鐘");
            Thread.Sleep(3000);
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
#endif

#if 沒有使用同步內容的非同步應用
        public async Task<ActionResult> Index()
        {
            Task myTask = Task.Run(() =>
            {
                Debug.WriteLine($"背景執行緒 休息 1 秒鐘");
                Thread.Sleep(1000);
                Debug.WriteLine($"背景執行緒 執行完畢");
            });

            Debug.WriteLine($"HTTP Request 休息 3 秒鐘");
            Thread.Sleep(3000);
            Debug.WriteLine($"HTTP Request 執行完畢");
            return View();
        }
#endif

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}