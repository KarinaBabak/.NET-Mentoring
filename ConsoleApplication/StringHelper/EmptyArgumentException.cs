using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringHelper
{
    [Serializable]
    public class EmptyArgumentException: ArgumentException
    {
        public EmptyArgumentException() { }

        public EmptyArgumentException(string message)
            : base(message)
        {

        }
    }

}
