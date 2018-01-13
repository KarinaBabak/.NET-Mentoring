using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Fibonachi.Interfaces;

namespace Fibonachi
{
    public class InMemoryCache : ICache<int>
    {
        private readonly string cachePrefix = "Default_";
        private readonly TimeSpan expirationInterval;
        private readonly ObjectCache cache = MemoryCache.Default;

        public InMemoryCache()
        {
            // Specify  Days, hours, minutes, seconds, milliseconds.
            expirationInterval = new TimeSpan(0, 8, 5, 30, 0);
        }

        public InMemoryCache(string cachePrefix, TimeSpan expirationInterval)
        {
            this.cachePrefix = cachePrefix;
            this.expirationInterval = expirationInterval;
        }

        public void SetValue(string key, IEnumerable<int> value)
        {
            if (value == null)
            {
                return;
            }

            cache.Set(cachePrefix + key, value, new DateTimeOffset(DateTime.UtcNow.Add(expirationInterval)));
        }

        public bool TryGetValue(string key, out IEnumerable<int> value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"Cached key {key} is not exist");
            }

            var cacheData = cache.Get(cachePrefix + key);
            if (cacheData != null)
            {
                value = cacheData as IEnumerable<int>;
                return true;
            }

            value = null;
            return false;
        }
    }
}
