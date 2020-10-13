using System;

namespace MT043StackOverflow造成的原因
{
    class Program
    {
        static void Main(string[] args)
        {
            int level = 1;
            Console.WriteLine("Hello World!");
            NestRecursionMethod(level);
        }

        private static void NestRecursionMethod(int level)
        {
            // 一個 decimal 使用 16 bytes 空間
            decimal decimal1=1;
            decimal decimal2= decimal1+1;
            decimal decimal3 = decimal2 + 1;
            decimal decimal4 = decimal3 + 1;
            decimal decimal5 = decimal4 + 1;
            Console.WriteLine($"Level {level}");
            NestRecursionMethod(++level);
        }
    }
}
