using Library.Custom_Exceptions;
using Library.Interfaces;
using Library.XmlWorkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Library
{
    public class Catalog
    {
        private const string catalogElementName = "catalog";

        protected List<BaseXmlWorker> XmlWorkers { get; set; }

        public Catalog(List<BaseXmlWorker> xmlWorkers)
        {
            XmlWorkers = xmlWorkers;
        }

        public void WriteToXmlFile(StringWriter stringWriter, IEnumerable<IEntity> catalogEntities)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings()))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(catalogElementName);

                foreach (var catalogEntity in catalogEntities)
                {
                    BaseXmlWorker entityXmlWorker = GetEntityXmlWorker(catalogEntity.GetType().Name);

                    try
                    {
                        entityXmlWorker.WriteEntity(xmlWriter, catalogEntity);
                    }
                    catch (ArgumentException exception)
                    {
                        throw exception;
                    }
                    catch (XmlWorkerNotFoundException exception)
                    {
                        throw exception;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }

                xmlWriter.WriteEndElement();
            }
        }

        public IEnumerable<IEntity> ReadFromXml(StringReader stringReader)
        {
            using (XmlReader xmlReader = XmlReader.Create(
                stringReader,
                new XmlReaderSettings
                {
                    IgnoreComments = true,
                    IgnoreWhitespace = true
                }))
            {
                xmlReader.ReadToFollowing(catalogElementName);
                xmlReader.ReadStartElement();

                do
                {
                    while (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        var node = XNode.ReadFrom(xmlReader) as XElement;
                        BaseXmlWorker entityXmlWorker = GetEntityXmlWorker(node.Name.LocalName);
                        yield return entityXmlWorker.ReadEntity(node);
                    }
                } while (xmlReader.Read());
            }
        }

        private BaseXmlWorker GetEntityXmlWorker(string entityKey)
        {
            BaseXmlWorker entityXmlWorker;
            entityXmlWorker = XmlWorkers.Find(w => w.EntityType.Name.ToLower() == entityKey.ToLower());

            if (entityXmlWorker == null)
            {
                throw new XmlWorkerNotFoundException($"There is no xml workers for entity { entityKey }");
            }

            return entityXmlWorker;
        }
    }
}
