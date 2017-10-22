using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringHelper
{
    public class StringHelper
    {
        private string inputString;

        public string InputValue
        {
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyArgumentException("You entered empty string");
                }

                this.inputString = value.Trim();
            }
        }

        public StringHelper()
        {

        }

        public char GetFirstStringSymbol()
        {
            return this.inputString[0];
        }
    }
}
