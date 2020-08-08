using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibraryService.Abstract;
using LibraryService.CatalogEntities;
using Shared;

namespace LibraryService.EntityParsers
{
    public class PaperParser : BaseEntityParser
    {
        public PaperParser()
        {
            EntityName = "paper";
        }

        public override BaseEntity Parse(XElement node)
        {
            if (node == null)
            {
                throw new NullReferenceException($"Node can't be null!");
            }

            var paper = new Paper()
            {
                Name = GetElementValue(node, "name"),
                City = GetElementValue(node, "city"),
                PublishHouse = GetElementValue(node, "publishHouse"),
                PublishYear = int.Parse(GetElementValue(node, "publishYear")),
                PageCount = int.Parse(GetElementValue(node, "pageCount")),
                Note = GetElementValue(node, "note"),
                Number = int.Parse(GetElementValue(node, "number")),
                Date = DateTime.Parse(GetElementValue(node, "date")),
                ISBN = GetElementValue(node, "ISBN")
            };

            return paper;
        }
    }
}
