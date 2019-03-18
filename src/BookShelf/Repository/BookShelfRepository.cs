using BookShelf.Repository.Entities;
using BookShelf.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BookShelf.Repository
{
    public class BookShelfRepository
    {
        private readonly IMongoCollection<BookShelfEntity> _collection;
        
        public BookShelfRepository(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.Database);
            _collection = database.GetCollection<BookShelfEntity>(mongoSettings.Value.Collection);
        }

        public IMongoQueryable<BookShelfEntity> GetBookShelf(string id)
        {
            var collection = _collection.AsQueryable()
                .Where(bsc => bsc.Id == id);
            return collection;
        }

        public IMongoQueryable<BookEntity> GetBook(string id)
        {
            var book = _collection.AsQueryable()
                .SelectMany(bs => bs.Books)
                .Where(b => b.Id == id);
            return book;
        }
    }
}