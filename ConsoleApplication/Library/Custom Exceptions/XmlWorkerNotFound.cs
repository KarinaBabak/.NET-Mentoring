using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Custom_Exceptions
{
    public class XmlWorkerNotFoundException : Exception
    {
        public XmlWorkerNotFoundException()
        {
        }

        public XmlWorkerNotFoundException(string message) : base(message)
        {
        }

        public XmlWorkerNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
