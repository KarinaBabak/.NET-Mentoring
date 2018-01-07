using Library.Entities;
using Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Library.XmlWorkers
{
    public class BookXmlWorker : BaseXmlWorker
    {
        public override Type EntityType => typeof(Book);

        public override string XmlElementName => "book";

        public override void WriteEntity(XmlWriter xmlWriter, IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"provided {nameof(entity)} is null or not of type {nameof(Book)}");
            }

            Book book = entity as Book;
            XElement element = new XElement(XmlElementName);

            SetAttributeValue(element, "isbn", book.IsbnNumber, true);
            AddElement(element, "name", book.Name);
            AddElement(element, "publishYear", book.PublishYear.ToString());
            AddElement(element, "pagesCount", book.PagesCount.ToString());
            AddElement(element, "note", book.Note);

            AddElement(element,
               new XElement("publisher",
                    new XAttribute("publisherName", book.Publisher?.Name),
                    new XAttribute("publisherCity", book.Publisher?.City)
            ));

            AddElement(element, "authors",
                book.Authors?.Select(a => new XElement("author",
                    new XAttribute("firstName", a.FirstName),
                    new XAttribute("lastName", a.LastName)
                ))
            );

            element.WriteTo(xmlWriter);
        }

        public override IEntity ReadEntity(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is not exist");
            }

            var publisher = GetElement(element, "publisher");

            Book book = new Book
            {
                Name = GetElement(element, "name").Value,
                IsbnNumber = GetAttributeValue(element, "isbn"),
                Publisher = new Publisher
                {
                    Name = GetAttributeValue(publisher, "publisherName"),
                    City = GetAttributeValue(publisher, "publisherCity")
                },
                Authors = GetElement(element, "authors").Elements("author").Select(e => new Author
                {
                    FirstName = GetAttributeValue(e, "firstName"),
                    LastName = GetAttributeValue(e, "lastName")
                }).ToList(),
                PagesCount = int.Parse(GetElement(element, "pagesCount").Value),
                Note = GetElement(element, "note").Value,
                PublishYear = int.Parse(GetElement(element, "publishYear").Value)
        };

            return book;
        }
    }
}
