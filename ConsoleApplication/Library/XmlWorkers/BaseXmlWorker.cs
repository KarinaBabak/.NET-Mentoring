using Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Library.XmlWorkers
{
    public abstract class BaseXmlWorker
    {
        public abstract Type EntityType { get; }
        public abstract string XmlElementName { get; }
        public abstract void WriteEntity(XmlWriter xmlWriter, IEntity entity);
        public abstract IEntity ReadEntity(XElement element);

        #region Helper Methods for wtiring elements

        protected void SetAttributeValue(XElement element, string attributeName, string value, bool isRequired = false)
        {
            if (string.IsNullOrEmpty(value) && isRequired)
            {
                throw new ArgumentException($"The attribute { attributeName } is required");
            }

            element.SetAttributeValue(attributeName, value);
        }

        protected void AddElement(XElement element, XElement newElement, bool isRequired = false)
        {
            if (newElement == null && isRequired)
            {
                throw new ArgumentException($"The element { newElement.Name } is required");
            }

            element.Add(newElement);
        }

        protected void AddElement(XElement element, string newElementName, object value, bool isRequired = false)
        {
            if ((value == null || (value as string) == string.Empty) && isRequired)
            {
                throw new ArgumentException($"The element { newElementName } is required");
            }

            var newElement = new XElement(newElementName, value);
            element.Add(newElement);
        }

        #endregion

        #region Helper Methods for reading elements

        protected XElement GetElement(XElement element, string elementName, bool isRequired = false)
        {
            var newElement = element.Element(elementName);

            if (string.IsNullOrEmpty(element?.Value) && isRequired)
            {
                throw new ArgumentException($"The element { element.Name } is required");
            }

            return newElement;
        }

        protected string GetAttributeValue(XElement element, string attributName, bool isRequired = false)
        {
            var attribute = element.Attribute(attributName);

            if (string.IsNullOrEmpty(attribute?.Value) && isRequired)
            {
                throw new ArgumentException($"The attribute { attributName } is required");
            }

            return attribute?.Value;
        }
        #endregion

        #region Helper Methods for dateTime values

        protected string WriteDate(DateTime dateTimeValue)
        {
            if (dateTimeValue == null)
            {
                return string.Empty;
            }

            return dateTimeValue.ToString("yyyy-MM-dd");
        }

        protected DateTime ReadDate(string dateTimeStringValue)
        {
            if (string.IsNullOrEmpty(dateTimeStringValue))
            {
                return default(DateTime);
            }

            return DateTime.Parse(dateTimeStringValue);
        }
        #endregion
    }
}
