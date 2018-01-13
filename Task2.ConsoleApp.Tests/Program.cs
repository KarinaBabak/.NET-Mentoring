using Caching;
using CachingSolutionsSamples;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2.ConsoleApp.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string categoriesPrefix = "Cache_Categories";
            string regionsPrefix = "Cache_Regions";
            string suppliersPrefix = "Cache_Suppliers";
            TimeSpan expirationInterval = new TimeSpan(0, 8, 5, 30, 0);

            var categoryManager = new EntitiesManager<Category>(new InMemoryCache<Category>(categoriesPrefix, expirationInterval));

            Console.WriteLine("Categories");

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetEntities().Count());
                Thread.Sleep(100);
            }


        }
    }
}
