using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task1.BookCatalog.Models
{
    public class Book
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "isbn")]
        public string Isbn { get; set; }

        [XmlElement(ElementName = "author", IsNullable = false)]
        public string Author { get; set; }

        [XmlElement(ElementName = "publish_date", DataType = "date", IsNullable = false)]
        public DateTime PublishDate { get; set; }

        [XmlElement(ElementName = "description", IsNullable = false)]
        public string Description { get; set; }

        [XmlElement(ElementName = "publisher", IsNullable = false)]
        public string Publisher { get; set; }

        [XmlElement(ElementName = "genre", IsNullable = false)]
        public Genre Genre { get; set; }

        [XmlElement(ElementName = "registration_date", DataType = "date", IsNullable = false)]
        public DateTime RegistrationDate { get; set; }

        public override string ToString()
        {
            return $"IsbnNumber - {Isbn}, Author - {Author}, Genre - {Genre}, Publisher - {Publisher}";
        }
    }
}
