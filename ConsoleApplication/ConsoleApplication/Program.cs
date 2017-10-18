using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputWorld;

            while(true)
            {
                try
                {
                    Console.WriteLine("Please input your minds: ");
                    inputWorld = Console.ReadLine();
                    if(String.IsNullOrWhiteSpace(inputWorld))
                    {

                    }
                    Console.WriteLine(inputWorld[0]);
                }
                catch(IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Please type something. Do not ignore!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("ArgumentException");
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }
    }
}
