using Fibonachi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonachiConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - In-process cache; 2 - Out-of-process cache;");
                Console.Write("Enter a key: ");
                var key = Console.ReadLine();
                Console.WriteLine();

                switch (key)
                {
                    case "1":
                        MemoryCache();
                        break;
                    case "2":
                        RedisCache();
                        break;
                    default:
                        return;
                }
            }

            Console.WriteLine();
        }

        private static void MemoryCache()
        {
            var generator = new Fibonachi.Fibonachi(new InMemoryCache());
            DisplayNumbers(generator.GetFibonachiNumbers(10));
        }

        private static void RedisCache()
        {
            var generator = new Fibonachi.Fibonachi(new RedisCache("localhost"));
            DisplayNumbers(generator.GetFibonachiNumbers(10));
        }

        private static void DisplayNumbers(IEnumerable<int> fibonachiNumbers)
        {
            foreach (var item in fibonachiNumbers)
            {
                Console.WriteLine(item);
            }
        }
    }
}
