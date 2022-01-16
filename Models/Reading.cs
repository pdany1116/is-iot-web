using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace IsIoTWeb.Models
{
    [BsonIgnoreExtraElements]
    public class Reading
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("collectorId")]
        public int CollectorId { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("soilMoistures")]
        public List<string> SoilMoistures  { get; set; }

        [BsonElement("airTemp")]
        public string AirTemp { get; set; }

        [BsonElement("airHummidity")]
        public string AirHummidity { get; set; }

        [BsonElement("lightIntensity")]
        public string LightIntensity { get; set; }
    }
}