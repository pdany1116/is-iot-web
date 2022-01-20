using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace IsIoTWeb.Models
{
    public class Valve : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("valveId")]
        public int ValveId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }
    }
}
