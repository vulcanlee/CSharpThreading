using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT023匿名的資料插槽DataSlots
{
    public class MyClass
    {
        public int MyInt;
        public string MyString;
        /// <summary>
        /// 封裝記憶體位置以儲存區域資料
        /// </summary>
        public static LocalDataStoreSlot localSlot;
        static MyClass()
        { localSlot = Thread.AllocateDataSlot(); }

        public static void Set(MyClass myClass)
        { Thread.SetData(localSlot, myClass); }
        public static MyClass Get()
        {
            MyClass myClass = Thread.GetData(localSlot) as MyClass;
            return myClass;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // 執行緒A
                Console.WriteLine("執行緒A設定 MyClass 靜態物件欄位值");
                MyClass.Set(new MyClass()
                {
                    MyInt = 10, // 數值型別欄位
                    MyString = "Vulcan", // 參考型別欄位
                });
                ShowStaticObject("執行緒A看到的 MyClass 靜態物件欄位值");
                Thread.Sleep(4000);
                ShowStaticObject("執行緒A看到的 MyClass 靜態物件欄位值");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // 執行緒B
                Thread.Sleep(2000);
                Console.WriteLine("執行緒B設定 MyClass 靜態物件欄位值");
                MyClass.Set(new MyClass()
                {
                    MyInt = 99, // 數值型別欄位
                    MyString = "Lee", // 參考型別欄位
                });
                ShowStaticObject("執行緒B看到的 MyClass 靜態物件欄位值");
            });
            Thread.Sleep(5000);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void ShowStaticObject(string str)
        {
            MyClass myClass = MyClass.Get();

            Console.WriteLine(str);
            Console.WriteLine($"MyClass.MyInt = {myClass.MyInt}");
            Console.WriteLine($"MyClass.MyString = {myClass.MyString}");
            Console.WriteLine();
        }
    }
}
