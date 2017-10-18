using FileSystemVisitor;
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
            //var directory = new Directory("D:/Epam/.NET Mentoring D1-D2/01. Advanced C#");
            var directory = new Directory("C:\\Program Files");
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();


            fileSystemVisitor.Start += HandleStartEvent;
            fileSystemVisitor.Finish += HandleFinishEvent;
            fileSystemVisitor.FileFound += HandleCustomEvent;
            fileSystemVisitor.DirectoryFound += HandleDirectoryFoundEvent;
            fileSystemVisitor.FilteredDirectoryFound += HandleFilteredDirectoryFoundEvent;
            fileSystemVisitor.FilteredFileFound += HandleFilteredFileFoundEvent;

            try
            {
                var files = directory.Accept(fileSystemVisitor);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }           

            Console.ReadKey();
        }

        public static void HandleStartEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Start searching");

        }

        public static void HandleFinishEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Finish searching");

        }

        public static void HandleFilteredFileFoundEvent(object sender, string fileName)
        {
            Console.WriteLine("Filtered File is found: {0}", fileName);
        }

        public static void HandleFilteredDirectoryFoundEvent(object sender, string directoryName)
        {
            Console.WriteLine("Filtered Directory is found: {0}", directoryName);
        }


        public static void HandleDirectoryFoundEvent(object sender, string directoryName)
        {
            Console.WriteLine("Directory is found: {0}", directoryName);

        }

        public static void HandleCustomEvent(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File is found: ");
            e.FilesToExclude = new string[] { "D:/Epam/.NET Mentoring D1-D2/01. Advanced C#\\02. Classes and Interfaces" + @"\Classes and Interfaces.mp4" };
        }
    }
}
