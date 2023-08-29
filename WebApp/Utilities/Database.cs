using MongoDB.Driver;
using MongoDB.Bson;
using Utilities.Models;

namespace Utilities
{
    public class Database
    {
        public const string DATABASE_NAME = "admin";
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
        private MongoClient GetClient()
        {
            string connectionString = GetConnectionString();
            var client = new MongoClient(connectionString);

            return client;
        }

        private IMongoDatabase GetDB()
        {
            var database = GetClient().GetDatabase(DATABASE_NAME);
            return database;
        }

        public bool SavePing(PingModel ping)
        {
            bool result = false;
            if(ping.PingTime.Year < 2023)
                ping.PingTime = DateTime.Now;
            var collection = GetDB().GetCollection<PingModel>("pings");
            collection.InsertOne(ping);
            result = true;
            return result;
        }

        public List<PingModel> GetPings()
        {
            List<PingModel> pings = new List<PingModel>();

            var collection = GetDB().GetCollection<PingModel>("pings");
            var filter = Builders<PingModel>.Filter.Empty;
            pings = collection.Find(filter).ToList();

            return pings;
        }

        public bool SaveSettings(SettingsModel settings)
        {
            bool result = false;

            var collection = GetDB().GetCollection<SettingsModel>("settings");
            var filter = Builders<SettingsModel>.Filter.Empty;
            var update = Builders<SettingsModel>.Update.Set(setting => setting.PingInterval, settings.PingInterval).Set(setting =>setting.ResetInterval, settings.ResetInterval);
            collection.UpdateOne(filter, update);
            result = true;

            return result;
        }

        public SettingsModel GetSettings()
        {
            SettingsModel settings = new SettingsModel();
            var collection = GetDB().GetCollection<SettingsModel>("settings");
            var filter = Builders<SettingsModel>.Filter.Empty;
            settings = collection.Find(filter).ToList().FirstOrDefault();

            return settings;
        }

        public List<string> GetLocationsForNotification()
        {
            List<string> locations = GetPings().Where(x => x.SendNotification == true).Select(x => x.Location).Distinct().ToList();

            return locations;
        }
        public PingModel GetLatestPing(string Location)
        {
            var ping = new PingModel();
            var collection = GetDB().GetCollection<PingModel>("pings");
            var sort = Builders<PingModel>.Sort.Descending(p => p.PingTime);
            var filter = Builders<PingModel>.Filter.Eq(p=>p.Location,Location);
            ping = collection.Find(filter).Sort(sort).FirstOrDefault();

            return ping;
        }
    }
}
