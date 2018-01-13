using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Interfaces
{
    public interface ICacheWithPolicy<T>
    {
        void SetValue(string key, IEnumerable<T> value, CacheItemPolicy policy = null);
    }
}
