using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringHelper;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            StringHelper.StringHelper strHelper = new StringHelper.StringHelper();

            while(true)
            {
                try
                {
                    Console.WriteLine("Please input your minds: ");
                    strHelper.InputValue = Console.ReadLine();
                    
                    Console.WriteLine(strHelper.GetFirstStringSymbol());
                }
                catch(IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Please type something. Do not ignore!");
                }
                catch (EmptyArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Incorrect string. Please type something! ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Incorrect word");
                }
            }

            Console.ReadKey();
        }
    }
}
