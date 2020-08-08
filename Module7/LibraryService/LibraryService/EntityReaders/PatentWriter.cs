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
    public class PatentWriter : BaseEntityWriter
    {
        public PatentWriter()
        {
            EntityType = typeof(Patent);
        }

        public override void WriteEntity(XmlWriter writer, BaseEntity entity)
        {
            if (!(entity is Patent patent))
            {
                throw new ArgumentException($"{nameof(entity)} is null or not a type of {nameof(Patent)}");
            }

            var element = new XElement("book");

            AddProperty(element, "name", patent.Name);
            AddProperty(element, "inventors",
                patent.Inventors?.Select(a => new XElement("inventor", a))
            );
            AddProperty(element, "city", patent.City);
            AddProperty(element, "registerNumber", patent.RegisterNumber);
            AddProperty(element, "requestDate", patent.RequestDate);
            AddProperty(element, "publishDate", patent.PublishDate);
            AddProperty(element, "pageCount", patent.PageCount);

            element.WriteTo(writer);
        }
    }
}
