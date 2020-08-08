using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibraryService;
using LibraryService.Abstract;
using LibraryService.EntityParsers;
using LibraryService.EntityReaders;
using Shared;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalogService = new CatalogService("catalog");
            catalogService.RegisterParsers(new BookParser(), new PaperParser(), new PatentParser());
            var result = catalogService.ReadCatalog();


            Book book = new Book()
            {
                Name = "1111",
            };
            
            catalogService.RegisterWriters(new BookWriter());

            StringBuilder str = new StringBuilder();
            StringWriter writer = new StringWriter(str);

            catalogService.WriteEntities(writer, new List<BaseEntity>(){book});

            var c = str.ToString();
        }
    }
}
