using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<BookShelfEntity>> GetBookShelvesAsync()
            => await _collection.Find(bs => true).ToListAsync();


        public async Task<BookShelfEntity> GetBookShelfAsync(Guid id)
            => await _collection.Find(bs => bs.Id == id).SingleOrDefaultAsync();


        public async Task AddBookShelfAsync(BookShelfEntity bookShelf)
            => await _collection.InsertOneAsync(bookShelf);

        public async Task DeleteBookShelfAsync(Guid id)
            => await _collection.DeleteOneAsync(bs => bs.Id == id);

        public async Task<bool> ExistsBookShelfAsync(Guid id)
            => await _collection.Find(bs => bs.Id == id).AnyAsync();

        public async Task<BookEntity> GetBookAsync(Guid bookId)
            => await _collection.AsQueryable()
                .SelectMany(bs => bs.Books)
                .Where(b => b.Id == bookId)
                .FirstOrDefaultAsync();
        
        public async Task<bool> ExistsBookAsync(Guid bookId)
            => await _collection.AsQueryable()
                .SelectMany(bs => bs.Books)
                .Where(b => b.Id == bookId)
                .AnyAsync();

        public async Task AddBookAsync(Guid bookShelfId, BookEntity book)
        {
            var filter = Builders<BookShelfEntity>.Filter.Where(bs => bs.Id == bookShelfId);
            var update = Builders<BookShelfEntity>.Update.Push(bs => bs.Books, book);
            await _collection.UpdateOneAsync(filter, update);
        }
        
        public async Task DeleteBookAsync(Guid bookShelfId, Guid bookId)
        {
            var filter = Builders<BookShelfEntity>.Filter.Where(bs => bs.Id == bookShelfId);
            var update = Builders<BookShelfEntity>.Update.PullFilter(
                bs => bs.Books,
                b => b.Id == bookId
            );
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}