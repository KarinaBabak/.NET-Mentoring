using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Task1.BookCatalog.Models
{
    [XmlRoot("catalog", Namespace = xmlNamespace)]
    public class Catalog
    {
        [XmlIgnore]
        public const string xmlNamespace = "http://library.by/catalog";

        [XmlElement("book")]
        public List<Book> Books { get; set; }

        [XmlAttribute(AttributeName = "date", DataType="date")]
        public DateTime Date { get; set; }
    }
}
