using MongoDB.Driver;
using MongoDB.Bson;
using Server.Models;

namespace Server.Classes
{
    public class Database
    {
        private string GetConnectionString()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            if (connectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }
            return connectionString;
        }
        public bool Test()
        {
            string connectionString = GetConnectionString();

            var client = new MongoClient(connectionString);

            var collection = client.GetDatabase("admin").GetCollection<BsonDocument>("pings");

            var filter = Builders<BsonDocument>.Filter.Empty;

            var document = collection.Find(filter).First();

            Console.WriteLine(document);

            return true;

        }

        public bool SavePing(PingModel ping)
        {
            bool result = false;

            string connectionString = GetConnectionString();

            var client = new MongoClient(connectionString);
            var collection = client.GetDatabase("admin").GetCollection<PingModel>("pings");
            collection.InsertOne(ping);
            result = true;
            return result;
        }

        public List<PingModel> GetPings()
        {
            List<PingModel> pings = new List<PingModel>();

            string connectionString = GetConnectionString();
            var client = new MongoClient(connectionString);
            var collection = client.GetDatabase("admin").GetCollection<PingModel>("pings");
            var filter = Builders<PingModel>.Filter.Empty;
            pings = collection.Find(filter).ToList();

            return pings;
        }

        public bool SaveSettings(SettingsModel settings)
        {
            bool result = false;

            string connectionString = GetConnectionString();
            var client = new MongoClient(connectionString);
            var collection = client.GetDatabase("admin").GetCollection<SettingsModel>("settings");
            var filter = Builders<SettingsModel>.Filter.Empty;
            var update = Builders<SettingsModel>.Update.Set(setting => setting.PingInterval, settings.PingInterval).Set(setting =>setting.ResetInterval, settings.ResetInterval);
            collection.UpdateOne(filter, update);
            result = true;

            return result;
        }

        public SettingsModel GetSettings()
        {
            SettingsModel settings = new SettingsModel();
            string connectionString = GetConnectionString();
            var client = new MongoClient(connectionString);
            var collection = client.GetDatabase("admin").GetCollection<SettingsModel>("settings");
            var filter = Builders<SettingsModel>.Filter.Empty;
            settings = collection.Find(filter).ToList().FirstOrDefault();

            return settings;
        }
    }
}
