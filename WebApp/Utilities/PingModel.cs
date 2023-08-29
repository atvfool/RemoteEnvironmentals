using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Utilities.Models
{
    public class PingModel
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("time")]
        public DateTime PingTime { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("humidity")]
        public Double Humidity { get; set; }
        [BsonElement("temp")]
        public Double Temperature { get; set; }
        [BsonElement("sendNotification")]
        public bool SendNotification { get; set; }
    }
}
