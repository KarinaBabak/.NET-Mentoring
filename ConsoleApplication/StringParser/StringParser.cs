using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringParser
{
    public static class StringParser
    {
        public static int ParseToInt(this string strToConvert)
        {
            if (strToConvert == null)
            {
                throw new ArgumentNullException("Input string is not exist");
            }

            strToConvert = strToConvert.Trim();

            if (String.IsNullOrWhiteSpace(strToConvert))
            {
                throw new ArgumentException("Input string is empty");
            }

            bool isNegativeValue = false;
            bool isPlusSymbolContain = false;
            char firstSymbol = strToConvert[0];

            if(firstSymbol == '-')
            {
                isNegativeValue = true;
            }
            else if(firstSymbol == '+')
            {
                isPlusSymbolContain = true;
            }

            int resultInt = 0;

            int startIndex = (isNegativeValue || isPlusSymbolContain) ? 1 : 0;
            for(int i = startIndex; i < strToConvert.Length; i++)
            {
                try
                {
                    if(!char.IsDigit(strToConvert[i]))
                    {
                        throw new FormatException("Input String can not be parsed: it contains not only digits");
                    }
  
                    checked
                    {
                        resultInt *= 10;

                        if (isNegativeValue)
                        {
                            resultInt -= (int)char.GetNumericValue(strToConvert[i]);
                        }
                        else
                        {
                            resultInt += (int)char.GetNumericValue(strToConvert[i]);
                        }
                    }
                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                catch (OverflowException ex)
                {
                    throw new OverflowException("Input string has a big length");
                }
                catch (Exception ex)
                {
                    throw new Exception("Parsing can not be finished");
                }

            }
            return resultInt;
        }
    }
}
