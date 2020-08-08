using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace LibraryService.Abstract
{
    public abstract class BaseEntityWriter
    {
        public Type EntityType { get; set; }

        public abstract void WriteEntity(XmlWriter writer, BaseEntity entity);

        public void AddProperty(XElement element, string propertyName, object propertyValue, bool isRequired = false)
        {
            if (propertyValue == null && isRequired)
            {
                throw new Exception($"Property {propertyName} is missing!");
            } 

            var property = new XElement(propertyName, propertyValue);
            element.Add(property);
        }
    }
}
