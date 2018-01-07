using Library.Entities;
using Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Library.XmlWorkers
{
    public class NewspaperXmlWorker : BaseXmlWorker
    {
        public override Type EntityType => typeof(Newspaper);

        public override string XmlElementName => "newspaper";

        public override IEntity ReadEntity(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is not exist");
            }

            var publisher = GetElement(element, "publisher");

            Newspaper newspaper = new Newspaper
            {
                Name = GetElement(element, "name").Value,
                Number = int.Parse(GetAttributeValue(element, "number")),
                Date = ReadDate(GetElement(element, "date").Value),
                IssnNumber = GetAttributeValue(element, "issnNumber"),
                Note = GetElement(element, "note").Value,
                PagesCount = int.Parse(GetElement(element, "pagesCount").Value),
                Publisher = new Publisher
                {
                    Name = GetAttributeValue(publisher, "publisherName"),
                    City = GetAttributeValue(publisher, "publisherCity")
                },
                PublishYear = int.Parse(GetElement(element, "publishYear").Value)
            };

            return newspaper;
        }

        public override void WriteEntity(XmlWriter xmlWriter, IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"Entity {nameof(entity)} to write is not exist");
            }

            Newspaper newspaper = entity as Newspaper;
            XElement element = new XElement(XmlElementName);

            SetAttributeValue(element, "issnNumber", newspaper.IssnNumber, true);
            SetAttributeValue(element, "number", newspaper.Number.ToString(), true);
            AddElement(element, "name", newspaper.Name, true);
            AddElement(element, "publishYear", newspaper.PublishYear.ToString());
            AddElement(element, "pagesCount", newspaper.PagesCount.ToString());
            AddElement(element, "note", newspaper.Note);
            AddElement(element, "date", WriteDate(newspaper.Date));

            AddElement(element,
               new XElement("publisher",
                    new XAttribute("publisherName", newspaper.Publisher?.Name),
                    new XAttribute("publisherCity", newspaper.Publisher?.City)
            ));

            element.WriteTo(xmlWriter);
        }
    }
}
