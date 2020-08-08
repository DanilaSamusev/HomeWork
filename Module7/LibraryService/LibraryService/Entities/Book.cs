using System;
using LibraryService.Abstract;

namespace Shared
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string[] Authors { get; set; }
        public string City { get; set; }
        public string PublishHouse { get; set; }
        public int PublishYear { get; set; }
        public int PageCount { get; set; }
        public string Note { get; set; }
        public string ISBN { get; set; }
    }
}
