using System;
using System.Linq;
using System.Xml.Linq;
using LibraryService.Abstract;
using Shared;

namespace LibraryService.EntityParsers
{
    public class PatentParser : BaseEntityParser
    {
        public PatentParser()
        {
            EntityName = "patent";
        }

        public override BaseEntity Parse(XElement node)
        {
            if (node == null)
            {
                throw new NullReferenceException($"Node can't be null!");
            }

            var patent = new Patent()
            {
                Name = GetElementValue(node, "name"),
                Inventors = node.Element("inventors")?.Elements("inventor").Select(elem => elem.Value).ToArray(),
                City = GetElementValue(node, "city"),
                RegisterNumber = GetElementValue(node, "registerNumber"),
                RequestDate = DateTime.Parse(GetElementValue(node, "requestDate")),
                PublishDate = DateTime.Parse(GetElementValue(node, "publishDate")),
                PageCount = int.Parse(GetElementValue(node, "pageCount"))
            };

            return patent;
        }
    }
}
