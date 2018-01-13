using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Task1.BookCatalog.Models;

namespace Task1.BookCatalog
{
    public static class CatalogXmlSerializer
    {

        public static void Serialize(Catalog catalog, string filePath)
        {
            var xml = new XmlSerializer(typeof(Catalog));

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                xml.Serialize(writer, catalog, GetCatalogNamespace(Catalog.xmlNamespace));
            }

        }

        public static Catalog Deserialize(string filePath)
        {
            var xml = new XmlSerializer(typeof(Catalog));

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                return xml.Deserialize(reader) as Catalog;
            }

        }

        private static XmlSerializerNamespaces GetCatalogNamespace(string xmlNamespace)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("xmlNamespace", xmlNamespace);
            return namespaces;
        }
    }
}
