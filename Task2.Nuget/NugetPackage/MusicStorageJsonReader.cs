using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml;

namespace NugetPackage
{
    public static class MusicStorageJsonReader
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
