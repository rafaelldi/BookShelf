using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BookShelf.Repository.Entities
{
    public class BookEntity
    {
        [BsonElement]
        public Guid Id { get; set; }
        [BsonElement]
        public string Title { get; set; }
        [BsonElement]
        public string Author { get; set; }
        [BsonElement]
        public string Comment { get; set; }
    }
}