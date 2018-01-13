using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;
using Caching;
using Caching.Interfaces;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
        private readonly string categoriesPrefix = "Cache_Categories";
        private readonly string regionsPrefix = "Cache_Regions";
        private readonly string suppliersPrefix = "Cache_Suppliers";
        private readonly TimeSpan expirationInterval = new TimeSpan(0, 8, 5, 30, 0);

        [TestMethod]
		public void CategoriesMemoryCache()
		{
            var categoryManager = new EntitiesManager<Category>(new InMemoryCache<Category>(categoriesPrefix, expirationInterval));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

        [TestMethod]
        public void RegionsMemoryCache()
        {
            var regionManager = new EntitiesManager<Region>(new InMemoryCache<Region>(regionsPrefix, expirationInterval));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(regionManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void SuppliersMemoryCache()
        {
            var supplierManager = new EntitiesManager<Supplier>(new InMemoryCache<Supplier>(suppliersPrefix, expirationInterval));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(supplierManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
		public void RedisCache()
		{
			var categoryManager = new EntitiesManager<Category>(new RedisCache<Category>("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}
	}
}
