using Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using StackExchange.Redis;
using System.IO;

namespace Caching
{
    public class RedisCache<T> : ICache<T>
    {
        private readonly string cachePrefix = "RedisCache_";
        private readonly ConnectionMultiplexer redisConnection;
        private readonly DataContractSerializer serializer;

        public RedisCache(string hostName)
        {
            var options = new ConfigurationOptions()
            {
                AbortOnConnectFail = false,
                EndPoints = { hostName }
            };

            redisConnection = ConnectionMultiplexer.Connect(options);
            serializer = new DataContractSerializer(typeof(IEnumerable<T>));
        }


        public void SetValue(string key, IEnumerable<T> value)
        {
            var db = redisConnection.GetDatabase();
            var fullKey = cachePrefix + key;

            if (value == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, value);
                db.StringSet(key, stream.ToArray());
            }
        }

        public bool TryGetValue(string key, out IEnumerable<T> value)
        {
            var db = redisConnection.GetDatabase();
            byte[] cacheData = db.StringGet(cachePrefix + key);
            if (cacheData != null)
            {
                value = (IEnumerable<T>)serializer.ReadObject(new MemoryStream(cacheData));
                return true;
            }

            value = null;
            return false;
        }
    }
}
