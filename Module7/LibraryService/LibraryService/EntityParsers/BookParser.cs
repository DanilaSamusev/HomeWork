using System;
using System.Linq;
using System.Xml.Linq;
using LibraryService.Abstract;
using Shared;

namespace LibraryService.EntityParsers
{
    public class BookParser : BaseEntityParser
    {
        public BookParser()
        {
            EntityName = "book";
        }

        public override BaseEntity Parse(XElement node)
        {
            if (node == null)
            {
                throw new NullReferenceException($"Node can't be null!");
            }

            Book book = new Book()
            {
                Name = GetElementValue(node, "name"),
                Authors = node.Element("authors")?.Elements("author").Select(elem => elem.Value).ToArray(),
                City = GetElementValue(node, "city"),
                PublishHouse = GetElementValue(node, "publishHouse"),
                PublishYear = int.Parse(GetElementValue(node, "publishYear")),
                PageCount = int.Parse(GetElementValue(node, "pageCount")),
                Note = GetElementValue(node, "note"),
                ISBN = GetElementValue(node, "ISBN", true),
            };

            return book;
        }
    }
}
