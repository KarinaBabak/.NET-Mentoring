using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi.Interfaces
{
    public interface ICache<T>
    {
        bool TryGetValue(string key, out IEnumerable<T> value);

        void SetValue(string key, IEnumerable<T> value);
    }
}
