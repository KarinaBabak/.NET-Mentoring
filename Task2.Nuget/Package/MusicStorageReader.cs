using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Package
{
    public static class MusicStorageReader
    {
        public static string ParseFromXMLToJson(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return JsonConvert.SerializeXmlNode(doc);
        }

        public static List<Track> ReadFromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<Track>>(jsonString);
        }
    }
}
