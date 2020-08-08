using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using LibraryService.Abstract;

namespace LibraryService
{
    public class CatalogService
    {
        private readonly string _catalogName;
        private readonly Dictionary<string, BaseEntityParser> _parsers = new Dictionary<string, BaseEntityParser>();
        private readonly Dictionary<Type, BaseEntityWriter> _writers = new Dictionary<Type, BaseEntityWriter>();

        public CatalogService(string catalogName)
        {
            _catalogName = catalogName;
        }

        public IEnumerable<BaseEntity> ReadCatalog()
        {
            using (XmlReader xmlReader = XmlReader.Create("Catalog.xml", new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            }))
            {
                xmlReader.ReadToFollowing(_catalogName);
                xmlReader.ReadStartElement();

                do
                {
                    while (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        var node = XNode.ReadFrom(xmlReader) as XElement;
                        BaseEntityParser parser = GetParser(node.Name.LocalName);
                        yield return parser.Parse(node);
                    }
                } while (xmlReader.Read());
            }
        }

        public void WriteEntities(TextWriter output, IEnumerable<BaseEntity> entities)
        {
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(_catalogName);

                foreach (var entity in entities)
                {
                    BaseEntityWriter entityWriter = _writers[entity.GetType()];

                    entityWriter.WriteEntity(writer, entity);
                }
            }
        }

        public void RegisterParsers(params BaseEntityParser[] parsers)
        {
            foreach (var parser in parsers)
            {
                _parsers.Add(parser.EntityName, parser);
            }
        }

        public void RegisterWriters(params BaseEntityWriter[] writers)
        {
            foreach (var writer in writers)
            {
                _writers.Add(writer.EntityType, writer);
            }
        }

        private BaseEntityParser GetParser(string nodeName)
        {
            BaseEntityParser parser = _parsers[nodeName];

            if (parser == null)
            {
                throw new Exception($"Invalid node name {nodeName}");
            }

            return _parsers[nodeName];
        }
    }
}
