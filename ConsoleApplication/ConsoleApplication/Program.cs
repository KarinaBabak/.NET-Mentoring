using CustomIoCContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypesLibrary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            var targetAssembly = Assembly.LoadFrom("TypesLibrary.dll");
            //var targetAssembly = Assembly.LoadFrom(@"D:\Epam\.NET Mentoring D1-D2\Tasks\Task5_Reflection\ConsoleApplication\TypesLibrary.dll");
            container.AddAssembly(targetAssembly);

            container.AddType(typeof(SuperClass), typeof(ISuperInterface));

            var instance = container.CreateInstance(typeof(SuperClass));
            Console.WriteLine(instance.GetType().ToString());


            Console.ReadKey();
        }
    }
}
