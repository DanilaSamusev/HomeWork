using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoBLL
{
    public class BookRepository
    {
        private const string DatabaseName = "BooksDB";
        private const string CollectionName = "Books";

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<Book> _collection;

        private List<Book> _books = new List<Book>
        {
            new Book
            {
                Name = "Hobbit", Author = "Tolkien", Count = 5, Genre = new List<string> {"fantasy"}, Year = 2014
            },
            new Book
            {
                Name = "Lord of the rings", Author = "Tolkien", Count = 3, Genre = new List<string> {"fantasy"}, Year = 2015
            },
            new Book
            {
                Name = "Kolobok", Count = 10, Genre = new List<string> {"kids"}, Year = 2000
            },
            new Book
            {
                Name = "Repka", Count = 11, Genre = new List<string> {"kids"}, Year = 2004
            },
            new Book
            {
                Name = "Dyadya Stiopa", Author = "Mihalkov", Count = 1, Genre = new List<string> {"kids"}, Year = 2001
            }
        };

        public BookRepository()
        {
            _client = new MongoClient();
            _db = _client.GetDatabase(DatabaseName);
            _collection = _db.GetCollection<Book>(CollectionName);
        }

        public void SetUpCollection()
        {
            _db.DropCollection(CollectionName);

            _collection.InsertMany(_books);
        }

        public IEnumerable<string> GetBookNamesWhereCountGreaterThanOne()
        {
            return _collection.AsQueryable()
                .Where(book => book.Count > 1)
                .Select(book => book.Name)
                .OrderBy(name => name)
                .Take(3);
        }

        public Book GetMaxCountBook()
        {
            int maxCount = _collection.AsQueryable().Max(book => book.Count);

            return _collection
                .AsQueryable()
                .FirstOrDefault(book => book.Count == maxCount);
        }

        public Book GetMinCountBook()
        {
            int maxCount = _collection.AsQueryable().Min(book => book.Count);

            return _collection
                .AsQueryable()
                .FirstOrDefault(book => book.Count == maxCount);
        }

        public IEnumerable<string> GetAuthors()
        {
            return _collection.AsQueryable()
                .Select(book => book.Author)
                .Distinct();
        }

        public IEnumerable<Book> GetBooksWithoutAuthors()
        {
            return _collection.AsQueryable()
                .Where(book => string.IsNullOrEmpty(book.Author));
        }

        public void IncreaseAllBooksCount()
        {
            var currentCollection = DropCollectionAndGetSource(_collection);

            currentCollection.ForEach(book => ++book.Count);

            _collection.InsertMany(currentCollection);
        }

        public void AddNewGenre()
        {
            var currentCollection = DropCollectionAndGetSource(_collection);

            currentCollection.ForEach(book =>
            {
                if (book.Genre.Any(genre => genre.Equals("fantasy"))
                    && book.Genre.Any(genre => !genre.Equals("favority")))
                {
                    book.Genre.Add("favority");
                }
            });

            _collection.InsertMany(currentCollection);
        }

        private List<Book> DropCollectionAndGetSource(IMongoCollection<Book> collection)
        {
            var currentCollection = _collection.AsQueryable().ToList();

            _db.DropCollection(CollectionName);

            return currentCollection;
        }
    }
}