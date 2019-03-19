using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace BookShelf.Repository.Entities
{
    public class BookShelfEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public List<BookEntity> Books { get; set; }
    }
}