using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT024ThreadLocal執行緒區域儲存區
{
    class MyClass
    {
        public static ThreadLocal<int> MyInt = 
            new ThreadLocal<int>(() => 168);
        public static ThreadLocal<string> MyString = 
            new ThreadLocal<string>(() => "Vulcan Lee");
    }

    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // 執行緒A
                Console.WriteLine("執行緒A設定 MyClass 靜態物件欄位值");
                MyClass.MyInt.Value = 10; // 數值型別欄位
                MyClass.MyString.Value = "Vulcan"; // 參考型別欄位
                ShowStaticObject("執行緒A看到的 MyClass 靜態物件欄位值");
                Thread.Sleep(4000);
                ShowStaticObject("執行緒A看到的 MyClass 靜態物件欄位值");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // 執行緒B
                Thread.Sleep(2000);
                Console.WriteLine("執行緒B查看 MyClass 靜態物件欄位初始值");
                ShowStaticObject("執行緒B看到的 MyClass 靜態物件欄位值");
                Console.WriteLine("執行緒B設定 MyClass 靜態物件欄位值");
                MyClass.MyInt.Value = 99; // 數值型別欄位
                MyClass.MyString.Value = "Lee"; // 參考型別欄位
                ShowStaticObject("執行緒B看到的 MyClass 靜態物件欄位值");
            });
            Thread.Sleep(5000);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void ShowStaticObject(string str)
        {
            Console.WriteLine(str);
            Console.WriteLine($"MyClass.MyInt = {MyClass.MyInt}");
            Console.WriteLine($"MyClass.MyString = {MyClass.MyString}");
            Console.WriteLine();
        }
    }
}
