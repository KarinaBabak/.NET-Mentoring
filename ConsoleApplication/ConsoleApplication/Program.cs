using Library;
using Library.Custom_Exceptions;
using Library.Entities;
using Library.Interfaces;
using Library.XmlWorkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFileName = "catalog.xml";
            #region init entities
            var book1 = new Book
            {
                Name = "The Picture of Dorian Gray",
                Publisher = new Publisher()
                {
                    City = "London",
                    Name = "Eksmo"
                },
                PublishYear = 2017,
                PagesCount = 320,
                IsbnNumber = "978-5-699-80202-9",
                Note = "In this celebrated work, his only novel, Wilde forged a devastating portrait of the effects of evil and debauchery on a young aesthete in late-19th-century England.",
                Authors = new List<Author>
                {
                    new Author {
                        FirstName = "Oskar",
                        LastName = "Wilde"
                    }
                }
            };

            var book2 = new Book
            {
                Name = "The perfect book",
                Publisher = new Publisher()
                {
                    City = "Minsk",
                    Name = "Mimimi"
                },
                PublishYear = 2018,
                PagesCount = 150,
                IsbnNumber = "978-5-699-22-9",
                Note = "Celebrated work",
                Authors = new List<Author>
                {
                    new Author {
                        FirstName = "Oskar",
                        LastName = "Mimo"
                    },
                     new Author {
                        FirstName = "Oskar",
                        LastName = "Lilo"
                    }
                }
            };

            var patent = new Patent
            {
                Name = "New Patent",
                RegistrationNumber = "123",
                Country = "USA",
                FillingDate = DateTime.Today,
                PublishDate = DateTime.Today,
                PagesCount = 50,
                Note = "In this celebrated work, his only novel, Wilde forged a devastating portrait of the effects of evil and debauchery on a young aesthete in late-19th-century England.",
                Inventors = new List<Inventor>
                {
                    new Inventor {
                        FirstName = "Oskar",
                        LastName = "Lilov"
                    }
                }
            };

            var newspaper = new Newspaper
            {
                Name = "Niva",
                Publisher = new Publisher()
                {
                    City = "Minsk",
                    Name = "Niva"
                },
                PublishYear = 2018,
                PagesCount = 9,
                Date = DateTime.Today,
                Note = "Actual news",
                IssnNumber = "123-456-78",
                Number = 2
            };
            #endregion

            List<IEntity> entities = new List<IEntity>
            {
                book1,
                book2,
                patent,
                newspaper
            };

            var xmlWorkers = new List<BaseXmlWorker>
            {
                new BookXmlWorker(),
                new NewspaperXmlWorker(),
                new PatentXmlWorker()
            };

            Catalog catalog = new Catalog(xmlWorkers);

            var bookXmlWorker = new BookXmlWorker();
            var newspaperXmlWorker = new NewspaperXmlWorker();
            var patentXmlWorker = new PatentXmlWorker();

            StringBuilder stringBuilder = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            { 
                try
                {
                    catalog.WriteToXmlFile(stringWriter, entities);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(stringBuilder.ToString());

                    doc.Save(xmlFileName);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (XmlWorkerNotFoundException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            #region Reading doc

            string content = File.ReadAllText(xmlFileName);

            using (StringReader stringReader = new StringReader(content))
            {
                try
                {
                    foreach (var item in catalog.ReadFromXml(stringReader))
                    {
                        Console.WriteLine(item.ToString());
                        Console.WriteLine();
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (XmlWorkerNotFoundException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            
            #endregion
            Console.WriteLine("The work is done");
            Console.ReadKey();
        }
    }
}
