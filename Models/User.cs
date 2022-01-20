using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace IsIoTWeb.Models
{
    public class User : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("authorities")]
        public List<string> Authorities { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
