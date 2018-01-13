using Caching;
using Caching.Interfaces;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public class EntitiesManager<T> where T : class
    {
        private ICache<T> cache;

        public EntitiesManager(ICache<T> cache)
        {
            this.cache = cache;
        }

        public IEnumerable<T> GetEntities()
        {
            Console.WriteLine("Get Entities");

            var user = Thread.CurrentPrincipal.Identity.Name;
            IEnumerable<T> entities = null;

            if (!cache.TryGetValue(user, out entities))
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                    cache.SetValue(user, entities);
                }
            }

            return entities;
        }
    }
}
