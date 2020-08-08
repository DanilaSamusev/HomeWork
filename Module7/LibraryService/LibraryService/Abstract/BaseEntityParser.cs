using System;
using System.Xml.Linq;

namespace LibraryService.Abstract
{
    public abstract class BaseEntityParser
    {
        public string EntityName { get; set; }

        public abstract BaseEntity Parse(XElement node);

        public string GetElementValue(XElement node, string parameterName, bool isRequired = false)
        {
            if (node == null)
            {
                throw new NullReferenceException($"Node can't be null!");
            }

            string value = node.Element(parameterName)?.Value;

            if (isRequired && string.IsNullOrEmpty(value))
            {
                throw new Exception($"{parameterName} is required field!");
            }

            return value;
        }
    }
}
