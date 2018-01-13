using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.BookCatalog;

namespace Task1.ConsoleApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFileName = "books.xml";

            var catalog = CatalogXmlSerializer.Deserialize(xmlFileName);

            foreach(var book in catalog.Books)
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine();

            xmlFileName = "books1.xml";

            CatalogXmlSerializer.Serialize(catalog, "books1.xml");
            var newCatalog = CatalogXmlSerializer.Deserialize(xmlFileName);

            foreach (var book in newCatalog.Books)
            {
                Console.WriteLine(book.ToString());
            }


            Console.ReadKey();
        }
    }
}
