using Fibonachi.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using StackExchange.Redis;
using System.IO;

namespace Fibonachi
{
    public class RedisCache : ICache<int>
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
            serializer = new DataContractSerializer(typeof(IEnumerable<int>));
        }


        public void SetValue(string key, IEnumerable<int> value)
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

        public bool TryGetValue(string key, out IEnumerable<int> value)
        {
            var db = redisConnection.GetDatabase();
            byte[] cacheData = db.StringGet(cachePrefix + key);
            if (cacheData != null)
            {
                value = (IEnumerable<int>)serializer.ReadObject(new MemoryStream(cacheData));
                return true;
            }

            value = null;
            return false;
        }
    }
}
