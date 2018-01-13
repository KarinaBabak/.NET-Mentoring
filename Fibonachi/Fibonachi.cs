using Fibonachi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi
{
    public class Fibonachi
    {
        private readonly ICache<int> cache;

        public Fibonachi()
        {
            this.cache = new InMemoryCache();
        }

        public Fibonachi(ICache<int> cache)
        {
            if(cache != null)
            {
                this.cache = cache;
            }
        }

        public IEnumerable<int> GetFibonachiNumbers(int count)
        {
            var date = DateTime.Now;
            var key = $"{date.Day}-{date.Month}-{date.Year}";

            IEnumerable<int> numbers;

            if(!cache.TryGetValue(key, out numbers))
            {
                numbers = FibonachiGenerator.GenerateClassicalFibs(count).ToList();
                cache.SetValue(key, numbers);
            }

            return numbers;
        } 
    }
}
