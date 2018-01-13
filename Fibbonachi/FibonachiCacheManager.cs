using Caching;
using Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Fibbonachi
{
    public class FibonachiCacheManager
    {
        private readonly ICache<int> cache;

        public FibonachiCacheManager()
        {
            this.cache = new InMemoryCache<int>();
        }

        public FibonachiCacheManager(ICache<int> cache)
        {
            if (cache != null)
            {
                this.cache = cache;
            }
        }

        public IEnumerable<int> GetFibonachiNumbers(int count)
        {
            var date = DateTime.Now;
            var key = $"{date.Day}-{date.Month}-{date.Year}";

            IEnumerable<int> numbers;

            if (!cache.TryGetValue(key, out numbers))
            {
                numbers = FibonachiGenerator.GenerateClassicalFibs(count).ToList();
                cache.SetValue(key, numbers);
            }

            return numbers;
        }
    }
}
