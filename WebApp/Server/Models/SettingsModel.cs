using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Server.Models
{
    public class SettingsModel
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("pingInterval")]
        public Double PingInterval { get; set; }
        [BsonElement("rebootInterval")]
        public Double ResetInterval { get; set; }
    }
}
