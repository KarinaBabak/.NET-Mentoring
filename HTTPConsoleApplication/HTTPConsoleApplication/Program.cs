using Custom_Wget;
using Custom_Wget.Enums;
using System;
using System.IO;

namespace HTTPConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            var url = "http://www.gnu.org/software/wget";//"https://yandex.by/";
            var directoryPath = @"C:\Test\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] fileExtensions = new string[] { ".html", ".png", ".jpg", ".gif" };
            CustomWget customWget = new CustomWget(directoryPath, 10, fileExtensions);
            customWget.LoadUrl(url, DomainRestriction.NoRestriction);

            Console.ReadKey();
        }
    }
}
