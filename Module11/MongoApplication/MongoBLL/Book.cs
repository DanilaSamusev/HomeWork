using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBLL
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public int Count { get; set; }

        public List<string> Genre { get; set; }

        public int Year { get; set; }
    }
}
