using System;
using System.Collections.Generic;

namespace MT044MemoryOverflow造成的原因
{
    public class MyLargeClass
    {
        // 這將會讓產生的執行個體會占用 > 85000 bytes 的記憶體大小
        public byte[] bytes = new byte[1024 * 86];
    }

    public class MySmallClass
    {
        // 這將會讓產生的執行個體會占用 < 85000 bytes 的記憶體大小
        public byte[] bytes = new byte[1024 * 20];
    }
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            //這裡將會模擬產生大量的大物件執行個體，耗用大量的堆積記憶體
            List<MyLargeClass> listLargeObject = new List<MyLargeClass>();
            for (int i = 0; i < 1000 * 1000; i++)
            {
                listLargeObject.Add(new MyLargeClass());
            }

            //這裡將會模擬產生大量的物件執行個體，耗用大量的堆積記憶體
            List<MySmallClass> listSmallObject = new List<MySmallClass>();
            for (int i = 0; i < 1000 * 1000; i++)
            {
                listSmallObject.Add(new MySmallClass());
            }
        }
    }
}
