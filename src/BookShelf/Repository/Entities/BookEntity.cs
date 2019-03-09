using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookShelf.Repository.Entities
{
    public class BookEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Title { get; set; }
        [BsonElement]
        public string Author { get; set; }
        [BsonElement]
        public string Comment { get; set; }
    }
}