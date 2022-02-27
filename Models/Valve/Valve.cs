using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IsIoTWeb.Models
{
    public class Valve : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("valveId")]
        public int ValveId { get; set; }

        [BsonElement("timestamp")]
        public double Timestamp { get; set; }

        [BsonElement("action")]
        public string Action { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }
    }
}
