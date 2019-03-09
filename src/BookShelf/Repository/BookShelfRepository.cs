using BookShelf.Repository.Entities;
using BookShelf.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BookShelf.Repository
{
    public class BookShelfRepository
    {
        private readonly IMongoCollection<BookShelfCollectionEntity> _collection;
        
        public BookShelfRepository(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.Database);
            _collection = database.GetCollection<BookShelfCollectionEntity>(mongoSettings.Value.Collection);
        }

        public IMongoQueryable<BookShelfCollectionEntity> GetBookShelfCollection(string id)
        {
            var collection = _collection.AsQueryable()
                .Where(bsc => bsc.Id == id);
            return collection;
        }

        public IMongoQueryable<BookShelfEntity> GetBookShelf(string id)
        {
            var bookShelf = _collection.AsQueryable()
                .SelectMany(bsc => bsc.BookShelves)
                .Where(bs => bs.Id == id);
            return bookShelf;
        }

        public IMongoQueryable<BookEntity> GetBook(string id)
        {
            var book = _collection.AsQueryable()
                .SelectMany(bsc => bsc.BookShelves)
                .SelectMany(bs => bs.Books)
                .Where(b => b.Id == id);
            return book;
        }
    }
}