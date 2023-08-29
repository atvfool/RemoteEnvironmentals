using Utilities.Models;
using Utilities;

Database db = new Database();
SettingsModel settings= db.GetSettings();
List<string> locations = db.GetLocationsForNotification();
foreach(string location in locations)
{
    PingModel ping = db.GetLatestPing(location);

    if(ping.Id.CreationTime.AddSeconds(settings.WatcherCheckInterval) <= DateTime.Now )
    {
        // Trigger alert
    }
}



