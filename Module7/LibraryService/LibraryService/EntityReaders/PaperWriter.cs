using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibraryService.Abstract;
using LibraryService.CatalogEntities;
using Shared;

namespace LibraryService.EntityReaders
{
    public class PaperWriter : BaseEntityWriter
    {
        public PaperWriter()
        {
            EntityType = typeof(Paper);
        }

        public override void WriteEntity(XmlWriter writer, BaseEntity entity)
        {
            if (!(entity is Paper paper))
            {
                throw new ArgumentException($"{nameof(entity)} is null or not a type of {nameof(Paper)}");
            }

            var element = new XElement("book");

            AddProperty(element, "name", paper.Name);
            AddProperty(element, "city", paper.City);
            AddProperty(element, "publishHouse", paper.PublishHouse);
            AddProperty(element, "publishYear", paper.PublishYear);
            AddProperty(element, "pageCount", paper.PageCount);
            AddProperty(element, "note", paper.Note);
            AddProperty(element, "number", paper.Number);
            AddProperty(element, "date", paper.Date);
            AddProperty(element, "ISBN", paper.ISBN);

            element.WriteTo(writer);
        }
    }
}
