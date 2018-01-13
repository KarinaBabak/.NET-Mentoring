using Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Caching
{
    public class InMemoryCache<T> : ICache<T>, ICacheWithPolicy<T>
    {
        private readonly string cachePrefix = "MemoryCache_";
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

        public void SetValue(string key, IEnumerable<T> value)
        {
            if (value == null)
            {
                return;
            }

            cache.Set(cachePrefix + key, value, new DateTimeOffset(DateTime.UtcNow.Add(expirationInterval)));
        }

        public void SetValue(string key, IEnumerable<T> value, CacheItemPolicy policy = null)
        {
            if (value == null)
            {
                return;
            }

            cache.Set(cachePrefix + key, value, policy);
        }

        public bool TryGetValue(string key, out IEnumerable<T> value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"Cached key {key} is not exist");
            }

            var cacheData = cache.Get(cachePrefix + key);
            if (cacheData != null)
            {
                value = cacheData as IEnumerable<T>;
                return true;
            }

            value = null;
            return false;
        }
    }
}
