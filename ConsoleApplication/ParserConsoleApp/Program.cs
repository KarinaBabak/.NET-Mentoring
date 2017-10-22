using System;
using System.Collections.Generic;
using System.Linq;
using StringParser;

namespace ParserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StringParser.StringParser.ParseToInt("-16985"));
            //Console.WriteLine(string.ParseToInt("55"));
            Console.ReadKey();
        }
    }
}
