using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT021ThreadStatic建立執行緒區域儲存區
{
    class MyClass
    {
        /// <summary>
        /// 執行緒相關的靜態欄位可提供最佳效能。 這也提供您在編譯時期檢查型別的好處。
        /// </summary>
        [ThreadStatic]
        public static int MyInt=168;
        [ThreadStatic]
        public static string MyString="Vulcan Lee";
    }
    // 您可使用 Managed 執行緒區域儲存區 (Thread Local Storage，TLS) 來儲存對執行緒及應用程式定義域來說是唯一的資料。
    // .NET Framework 提供兩種方法來使用 Managed TLS：執行緒相關的靜態欄位和資料位置。
    // https://msdn.microsoft.com/zh-tw/library/system.threadstaticattribute(v=vs.110).aspx

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
                Console.WriteLine("執行緒B查看 MyClass 靜態物件欄位初始值");
                ShowStaticObject("執行緒B看到的 MyClass 靜態物件欄位值");
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
