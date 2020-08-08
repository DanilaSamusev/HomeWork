using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibraryService.Abstract;
using Shared;

namespace LibraryService.EntityReaders
{
    public class BookWriter : BaseEntityWriter
    {
        public BookWriter()
        {
            EntityType = typeof(Book);
        }

        public override void WriteEntity(XmlWriter writer, BaseEntity entity)
        {
            if (!(entity is Book book))
            {
                throw new ArgumentException($"{nameof(entity)} is null or not a type of {nameof(Book)}");
            }

            var element = new XElement("book");

            AddProperty(element, "name", book.Name);
            AddProperty(element, "authors",
                book.Authors?.Select(a => new XElement("author", a))
            );
            AddProperty(element, "city", book.City);
            AddProperty(element, "publishHouse", book.PublishHouse);
            AddProperty(element, "publishYear", book.PublishYear);
            AddProperty(element, "pageCount", book.PageCount);
            AddProperty(element, "note", book.Note);
            AddProperty(element, "ISBN", book.ISBN);

            element.WriteTo(writer);
        }
    }
}
