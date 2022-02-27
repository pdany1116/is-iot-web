using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IsIoTWeb.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}
