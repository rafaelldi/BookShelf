using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookShelf.Repository.Entities
{
    public class BookShelfCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement]
        public string UserEmail { get; set; }
    }
}