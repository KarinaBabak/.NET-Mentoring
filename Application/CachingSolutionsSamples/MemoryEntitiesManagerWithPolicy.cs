using Caching;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public class MemoryEntitiesManagerWithPolicy<T> where T : class
    {
        private readonly InMemoryCache<T> cache;
        private readonly string monitorCommand;

        public MemoryEntitiesManagerWithPolicy(InMemoryCache<T> cache, string monitorCommand)
        {
            this.cache = cache;
            this.monitorCommand = monitorCommand;
        }

        public IEnumerable<T> GetEntities()
        {
            Console.WriteLine("Get Entities");
            var user = Thread.CurrentPrincipal.Identity.Name;
            IEnumerable<T> entities = null;
            string connectionString;

            if (!cache.TryGetValue(user, out entities))
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                    connectionString = dbContext.Database.Connection.ConnectionString;
                }

                SqlDependency.Start(connectionString);
                cache.SetValue(user, entities, GetCachePolicy(monitorCommand, connectionString));
            }

            return entities;
        }

        private CacheItemPolicy GetCachePolicy(string monitorCommand, string connectionString)
        {
            return new CacheItemPolicy
            {
                ChangeMonitors = { GetMonitor(monitorCommand, connectionString) }
            };
        }

        private SqlChangeMonitor GetMonitor(string query, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                var monitor = new SqlChangeMonitor(new SqlDependency(command));
                command.ExecuteNonQuery();
                return monitor;
            }
        }
    }
}
