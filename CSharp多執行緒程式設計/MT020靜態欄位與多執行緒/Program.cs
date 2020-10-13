using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT020靜態欄位與多執行緒
{
    class MyClass
    {
        public static int MyInt;
        public static string MyString;
    }
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // 執行緒A
                Console.WriteLine("執行緒A設定 MyClass 靜態物件欄位值");
                MyClass.MyInt = 10; // 數值型別欄位
                MyClass.MyString = "Vulcan"; // 參考型別欄位
                ShowStaticObject("執行緒A看到的 MyClass 靜態物件欄位值");
                Thread.Sleep(4000);
                ShowStaticObject("執行緒A看到的 MyClass 靜態物件欄位值");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // 執行緒B
                Thread.Sleep(2000);
                Console.WriteLine("執行緒B設定 MyClass 靜態物件欄位值");
                MyClass.MyInt = 99; // 數值型別欄位
                MyClass.MyString = "Lee"; // 參考型別欄位
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
