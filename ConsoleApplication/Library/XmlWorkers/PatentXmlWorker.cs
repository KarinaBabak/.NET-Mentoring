using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Library.Interfaces;
using Library.Entities;

namespace Library.XmlWorkers
{
    public class PatentXmlWorker : BaseXmlWorker
    {
        public override Type EntityType => typeof(Patent);

        public override string XmlElementName => "patent";

        public override IEntity ReadEntity(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is not exist");
            }

            Patent patent = new Patent
            {
                Name = GetElement(element, "name").Value,
                Country = GetElement(element, "country").Value,
                Note = GetElement(element, "note").Value,
                FillingDate = ReadDate(GetElement(element, "fillingDate").Value),
                PublishDate = ReadDate(GetElement(element, "publishDate").Value),
                PagesCount = int.Parse(GetAttributeValue(element, "pagesCount")),
                RegistrationNumber = GetAttributeValue(element, "pagesCount"),
                Inventors = GetElement(element, "inventors").Elements("inventor").Select(e => new Inventor
                {
                    FirstName = GetAttributeValue(e, "firstName"),
                    LastName = GetAttributeValue(e, "lastName")
                }).ToList(),
            };

            return patent;
        }

        public override void WriteEntity(XmlWriter xmlWriter, IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"provided {nameof(entity)} is null or not of type {nameof(Book)}");
            }

            Patent patent = entity as Patent;
            XElement element = new XElement(XmlElementName);

            SetAttributeValue(element, "registrationNumber", patent.RegistrationNumber, true);
            SetAttributeValue(element, "pagesCount", patent.PagesCount.ToString());
            AddElement(element, "name", patent.Name);
            AddElement(element, "country", patent.Country);
            AddElement(element, "note", patent.Note);
            AddElement(element, "fillingDate", WriteDate(patent.FillingDate));
            AddElement(element, "publishDate", WriteDate(patent.PublishDate));
            AddElement(element, "inventors",
                patent.Inventors?.Select(i => new XElement("inventor",
                    new XAttribute("firstName", i.FirstName),
                    new XAttribute("lastName", i.LastName)
                ))
            );

            element.WriteTo(xmlWriter);
        }
    }
}
