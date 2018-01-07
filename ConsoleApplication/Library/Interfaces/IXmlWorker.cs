using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Library.Interfaces
{
    public interface IXmlWorker
    {
        void WriteEntity(XmlWriter xmlWriter, IEntity entity);
        IEntity ReadEntity(XElement element);
    }
}
