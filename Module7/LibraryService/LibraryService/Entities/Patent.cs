using System;
using LibraryService.Abstract;

namespace Shared
{
    public class Patent : BaseEntity
    {
        public string Name { get; set; }
        public string[] Inventors { get; set; }
        public string City { get; set; }
        public string RegisterNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int PageCount { get; set; }
    }
}
