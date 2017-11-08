using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CustomFilesWatcher;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomFilesWatcher.CustomFileSystemWatcher fileSystemWatcher = new CustomFilesWatcher.CustomFileSystemWatcher();
            //CustomFileSystemWatcher.CustomFileSystemWatcher fileSystemWatcher = new CustomFileSystemWatcher.CustomFileSystemWatcher();
            Console.ReadKey();
        }
    }
}
