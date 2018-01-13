using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi
{
    public static class FibonachiGenerator
    {
        public static IEnumerable<int> GenerateClassicalFibs(int count)
        {
            if (count < 2) throw new ArgumentException("Count of elements can be more or quals Two");

            int newElement = 0;

            for (int i = 0, first = 1, second = 1; i < count; i++)
            {
                yield return first;
                newElement = first + second;
                first = second;
                second = newElement;
            }
        }
    }
}
