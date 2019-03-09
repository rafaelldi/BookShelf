using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookShelf.Repository.Entities
{
    public class BookShelfEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public List<BookEntity> Books { get; set; }
    }
}