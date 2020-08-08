using System;
using LibraryService.Abstract;

namespace LibraryService.CatalogEntities
{
    public class Paper : BaseEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string PublishHouse { get; set; }
        public int PublishYear { get; set; }
        public int PageCount { get; set; }
        public string Note { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISBN { get; set; }
    }
}
