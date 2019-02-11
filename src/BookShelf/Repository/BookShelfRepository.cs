using BookShelf.Repository.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookShelf.Repository
{
    public class BookShelfRepository
    {
        private readonly IMongoCollection<BookShelfCollection> _collection;
        
        public BookShelfRepository(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.Database);
            _collection = database.GetCollection<BookShelfCollection>(mongoSettings.Value.Collection);
        }
    }
}