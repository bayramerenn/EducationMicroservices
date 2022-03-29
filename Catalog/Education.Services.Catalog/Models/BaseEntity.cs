using MongoDB.Bson.Serialization.Attributes;

namespace Education.Services.Catalog.Models
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
    }
}